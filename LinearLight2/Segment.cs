using System;

namespace LinearLight2
{
    public class Segment
    {
        public const ushort LightStatusDiscreteInput = 1000 - 1;
        public const ushort HardwareTriggerDiscreteInput = 1001 - 1;
        public const ushort BodyOverheatDiscreteInput = 1101 - 1;
        public const ushort LedOverheatDiscreteInput = 1201 - 1;
        public const ushort SetIntensity1HoldingRegister = 4215 - 1;
        public const ushort SetIntensity2HoldingRegister = 4225 - 1;
        public const ushort FanSpeedHoldingRegister = 4109 - 1;
        public const ushort ConfigurationHoldingRegister = 4090 - 1;
        public const ushort InputSettingsHoldingRegister = 4000 - 1;
        public const ushort SwTriggerCoil = 2000 - 1;
        public const ushort FanEnableCoil = 2001 - 1;
        public const ushort ProtocolVersionInputRegister = 3000 - 1;
        public const ushort SoftwareVersionInputRegister = 3001 - 1;
        public const ushort HardwareVersionInputRegister = 3002 - 1;
        public const ushort SnHighInputRegister = 3003 - 1;
        public const ushort SnLowInputRegister = 3004 - 1;
        public const ushort PnHighInputRegister = 3005 - 1;
        public const ushort PnLowInputRegister = 3006 - 1;
        public const ushort BodyTemperatureInputRegister = 3101 - 1;
        public const ushort BodyMaxTemperatureInputRegister = 3102 - 1;
        public const ushort FanCurrentRpmInputRegister = 3109 - 1;
        public const ushort LedTemperatureInputRegister = 3201 - 1;
        public const ushort LedMaxTemperatureInputRegister = 3202 - 1;
        public const ushort LuxMeterValueInputRegister = 3208 - 1;
        public const ushort Voltage1InputRegister = 3213 - 1;
        public const ushort Current1InputRegister = 3214 - 1;
        public const ushort Voltage2InputRegister = 3223 - 1;
        public const ushort Current2InputRegister = 3224 - 1;

        private byte slaveAddress;
        private IModbusMaster modbusMaster;

        public Segment(IModbusMaster modbusMaster, byte slaveAddress)
        {
            this.modbusMaster = modbusMaster;
            this.slaveAddress = slaveAddress;
        }

        public bool LightOnFlag => ReadDiscreteInputsFromSegment(LightStatusDiscreteInput);
        public bool HardwareTriggerStatus => ReadDiscreteInputsFromSegment(HardwareTriggerDiscreteInput);
        public bool BodyOverheatFlag => ReadDiscreteInputsFromSegment(BodyOverheatDiscreteInput);
        public bool LedOverheatFlag => ReadDiscreteInputsFromSegment(LedOverheatDiscreteInput);

        public int ProtocolVersion => ReadInputRegisterFromSegment(ProtocolVersionInputRegister);
        public int SoftwareVersion => ReadInputRegisterFromSegment(SoftwareVersionInputRegister);
        public int HardwareVersion => ReadInputRegisterFromSegment(HardwareVersionInputRegister);
        public uint SerialNumber => ReadUInt32SFromSegment(SnHighInputRegister, SnLowInputRegister);

        public uint ProductNumber => ReadUInt32SFromSegment(PnHighInputRegister, PnLowInputRegister);
        public int BodyTemperature => ReadInputRegisterFromSegment(BodyTemperatureInputRegister);
        public int BodyMaxTemperature => ReadInputRegisterFromSegment(BodyMaxTemperatureInputRegister);
        public int FanCurrentRpm => ReadInputRegisterFromSegment(FanCurrentRpmInputRegister, "Reading fan rmp failed. Note that reading has been implemented since protocol version 1.01.");

        public int LedTemperature => ReadInputRegisterFromSegment(LedTemperatureInputRegister);
        public int LedMaxTemperature => ReadInputRegisterFromSegment(LedMaxTemperatureInputRegister);
        public int LuxValue => ReadInputRegisterFromSegment(LuxMeterValueInputRegister);

        public bool FanEnable => ReadCoilFromSegment(FanEnableCoil);

        public double Amperes1 =>
            ReadInputRegisterFromSegment(Current1InputRegister) / 1000.0;

        public double Amperes2 =>
            ReadInputRegisterFromSegment(Current2InputRegister)/ 1000.0;

        public double Volts1 =>
            ReadInputRegisterFromSegment(Voltage1InputRegister)/ 1000.0;

        public double Volts2 =>
            ReadInputRegisterFromSegment(Voltage2InputRegister) / 1000.0;

        
        public int SetIntensity1 => ReadHoldingRegisterFromSegment(SetIntensity1HoldingRegister);
        public int SetIntensity2 => ReadHoldingRegisterFromSegment(SetIntensity2HoldingRegister);

        public ConfigurationStatus ConfigurationRegister =>
            (ConfigurationStatus) ReadHoldingRegisterFromSegment(ConfigurationHoldingRegister);


        private int ReadHoldingRegisterFromSegment(ushort address)
        {
            return modbusMaster.ReadHoldingRegisters(slaveAddress, address, 1)[0];
        }

        private int ReadInputRegisterFromSegment(ushort address)
        {
            return ReadInputRegisterFromSegment(address, "Input register read error. Check that requested value is supported by the protocol number light is implementing.");
        }

        private int ReadInputRegisterFromSegment(ushort address, string exceptionMessage)
        {
            try
            {
                return modbusMaster.ReadInputRegisters(slaveAddress, address, 1)[0];
            }
            catch (Exception e)
            {
                throw new Exception(exceptionMessage, e);
            }
        }

        private uint ReadUInt32SFromSegment(ushort addressHigh, ushort addressLow)
        {

            var high = modbusMaster.ReadInputRegisters(slaveAddress, addressHigh, 1)[0];
            var low = modbusMaster.ReadInputRegisters(slaveAddress, addressLow, 1)[0];
            return (((uint) high) << 16) | low;
        }

        private bool ReadDiscreteInputsFromSegment(ushort address)
        {
            return modbusMaster.ReadDiscreteInputs(slaveAddress, address, 1)[0];
        }

        private bool ReadCoilFromSegment(ushort address)
        {
            return modbusMaster.ReadCoils(slaveAddress, address, 1)[0];
        }
    }
}