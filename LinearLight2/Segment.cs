using System;

namespace LinearLight2
{
    public class Segment : ISegment
    {
        public const ushort LightStatusDiscreteInput = 1000 - 1;
        public const ushort HardwareTriggerDiscreteInput = 1001 - 1;
        public const ushort BodyOverheatDiscreteInput = 1101 - 1;
        public const ushort LedOverheatDiscreteInput = 1201 - 1;
        public const ushort SetIntensity1HoldingRegister = 4215 - 1;
        public const ushort SetIntensity2HoldingRegister = 4225 - 1;
        public const ushort FanSpeedHoldingRegister = 4109 - 1;
        public const ushort FanModeHoldingRegister = 4110 - 1;
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
        public const ushort FanSetSpeedInputRegister = 3110 - 1;
        public const ushort FanAutoMinSpeedInputRegister = 3111 - 1;
        public const ushort FanAutoMaxSpeedInputRegister = 3112 - 1;
        public const ushort FanAutoMinTempInputRegister = 3113 - 1;
        public const ushort FanAutoMaxTempInputRegister = 3114 - 1;
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

        public bool FanEnable
        {
            get => ReadCoilFromSegment(FanEnableCoil);
            set => WriteSingleCoil(FanEnableCoil, value);
        }

        public bool SwTrigger
        {
            get => ReadCoilFromSegment(SwTriggerCoil);
            set => WriteSingleCoil(SwTriggerCoil, value);
        }

        public int ProtocolVersion => ReadInputRegisterFromSegment(ProtocolVersionInputRegister);
        public int SoftwareVersion => ReadInputRegisterFromSegment(SoftwareVersionInputRegister);
        public int HardwareVersion => ReadInputRegisterFromSegment(HardwareVersionInputRegister);
        public string SerialNumber => ReadStringFromSegments(SnHighInputRegister, SnLowInputRegister);

        public string ProductNumber => ReadStringFromSegments(PnHighInputRegister, PnLowInputRegister);
        public int BodyTemperature => ReadInputRegisterFromSegment(BodyTemperatureInputRegister);
        public int BodyMaxTemperature => ReadInputRegisterFromSegment(BodyMaxTemperatureInputRegister);

        public int FanCurrentRpm => ReadInputRegisterFromSegment(FanCurrentRpmInputRegister,
                                                                 "Reading fan rmp failed. Note that reading has been implemented since protocol version 1.01.");

        public int FanSetSpeed => ReadInputRegisterFromSegment(FanSetSpeedInputRegister);
        public int FanAutoMinSpeed => ReadInputRegisterFromSegment(FanAutoMinSpeedInputRegister);
        public int FanAutoMaxSpeed => ReadInputRegisterFromSegment(FanAutoMaxSpeedInputRegister);
        public int FanAutoMinTemp => ReadInputRegisterFromSegment(FanAutoMinTempInputRegister);
        public int FanAutoMaxTemp => ReadInputRegisterFromSegment(FanAutoMaxTempInputRegister);

        public int LedTemperature => ReadInputRegisterFromSegment(LedTemperatureInputRegister);
        public int LedMaxTemperature => ReadInputRegisterFromSegment(LedMaxTemperatureInputRegister);
        public int LuxValue => ReadInputRegisterFromSegment(LuxMeterValueInputRegister);

        public double Amperes1 =>
            ReadInputRegisterFromSegment(Current1InputRegister) / 1000.0;

        public double Amperes2 =>
            ReadInputRegisterFromSegment(Current2InputRegister) / 1000.0;

        public double Volts1 =>
            ReadInputRegisterFromSegment(Voltage1InputRegister) / 1000.0;

        public double Volts2 =>
            ReadInputRegisterFromSegment(Voltage2InputRegister) / 1000.0;


        public FanMode FanMode
        {
            get => (FanMode) ReadHoldingRegisterFromSegment(FanModeHoldingRegister);
            set => WriteSingleRegister(FanModeHoldingRegister, (ushort) value);
        }

        public int SetIntensity1
        {
            get => ReadHoldingRegisterFromSegment(SetIntensity1HoldingRegister);
            set => WriteSingleRegister(SetIntensity1HoldingRegister, (ushort) value);
        }

        public int SetIntensity2
        {
            get => ReadHoldingRegisterFromSegment(SetIntensity2HoldingRegister);
            set => WriteSingleRegister(SetIntensity2HoldingRegister, (ushort) value);
        }

        public ConfigurationStatus ConfigurationRegister
        {
            get => (ConfigurationStatus) ReadHoldingRegisterFromSegment(ConfigurationHoldingRegister);
            set => WriteSingleRegister(ConfigurationHoldingRegister, (ushort) value);
        }

        public int SetFanSpeed
        {
            get => ReadHoldingRegisterFromSegment(FanSpeedHoldingRegister);
            set => WriteSingleRegister(FanSpeedHoldingRegister, (ushort) value);
        }

        public TriggerMode TriggerMode
        {
            get => (TriggerMode) ReadHoldingRegisterFromSegment(InputSettingsHoldingRegister);
            set => WriteSingleRegister(InputSettingsHoldingRegister, (ushort) value);
        }


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

        private string ReadStringFromSegments(ushort addressHigh, ushort addressLow)
        {
            var high = modbusMaster.ReadInputRegisters(slaveAddress, addressHigh, 1)[0];
            var low = modbusMaster.ReadInputRegisters(slaveAddress, addressLow, 1)[0];

            var highStr = high.ToString("X4");
            var lowStr = low.ToString("X4");
            return $"{highStr}{lowStr}";
        }

        private bool ReadDiscreteInputsFromSegment(ushort address)
        {
            return modbusMaster.ReadDiscreteInputs(slaveAddress, address, 1)[0];
        }

        private bool ReadCoilFromSegment(ushort address)
        {
            return modbusMaster.ReadCoils(slaveAddress, address, 1)[0];
        }

        private void WriteSingleCoil(ushort address, bool value)
        {
            modbusMaster.WriteSingleCoil(slaveAddress, address, value);
        }

        private void WriteSingleRegister(ushort address, ushort value)
        {
            modbusMaster.WriteSingleRegister(slaveAddress,address, value);
        }
    }
}