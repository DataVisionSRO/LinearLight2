using System;
using System.Collections.Generic;
using System.Linq;

namespace LinearLight2
{
    public class LinearLight
    {

        private readonly IModbusMaster modbusMaster;

        private const ushort LightStatusDiscreteInput = 1000 - 1;
        private const ushort HardwareTriggerDiscreteInput = 1001 - 1;
        private const ushort BodyOverheatDiscreteInput = 1101 - 1;
        private const ushort LedOverheatDiscreteInput = 1201 - 1;
        private const ushort SetIntensity1HoldingRegister = 4215 - 1;
        private const ushort SetIntensity2HoldingRegister = 4225 - 1;
        private const ushort FanSpeedHoldingRegister = 4109 - 1;
        private const ushort InputSettingsHoldingRegister = 4000 - 1;
        private const ushort SwTriggerCoil = 2000 - 1;
        private const ushort FanEnableCoil = 2001 - 1;
        private const ushort ProtocolVersionInputRegister = 3000 - 1;
        private const ushort SoftwareVersionInputRegister = 3001 - 1;
        private const ushort HardwareVersionInputRegister = 3002 - 1;
        private const ushort SnHighInputRegister = 3003 - 1;
        private const ushort SnLowInputRegister = 3004 - 1;
        private const ushort PnHighInputRegister = 3005 - 1;
        private const ushort PnLowInputRegister = 3006 - 1;
        private const ushort BodyTemperatureInputRegister = 3101 - 1;
        private const ushort BodyMaxTemperatureInputRegister = 3102 - 1;
        private const ushort FanCurrentRpmInputRegister = 3109 - 1;
        private const ushort LedTemperatureInputRegister = 3201 - 1;
        private const ushort LedMaxTemperatureInputRegister = 3202 - 1;
        private const ushort LuxMeterValueInputRegister = 3208 - 1;
        private const ushort Voltage1InputRegister = 3213 - 1;
        private const ushort Current1InputRegister = 3214 - 1;
        private const ushort Voltage2InputRegister = 3223 - 1;
        private const ushort Current2InputRegister = 3224 - 1;
        private readonly int segmentCount;
        private readonly byte slaveBaseAddress;


        public LinearLight(IModbusMaster master, int segmentCount) : this(master, segmentCount, 1)
        {

        }
        public LinearLight(IModbusMaster master, int segmentCount, byte startAddr)
        {
            this.segmentCount = segmentCount;
            modbusMaster = master;
            slaveBaseAddress = startAddr;
        }


        public IEnumerable<bool> LightOnFlags => ReadDiscreteInputsFromSegments(LightStatusDiscreteInput);
        public IEnumerable<bool> HardwareTriggerStatus => ReadDiscreteInputsFromSegments(HardwareTriggerDiscreteInput);
        public IEnumerable<bool> BodyOverheatFlags => ReadDiscreteInputsFromSegments(BodyOverheatDiscreteInput);
        public IEnumerable<bool> LedOverheatFlags => ReadDiscreteInputsFromSegments(LedOverheatDiscreteInput);

        public int Intensity
        {
            set
            {
                modbusMaster.BroadcastWriteSingleRegister(SetIntensity1HoldingRegister, (ushort) value);
                modbusMaster.BroadcastWriteSingleRegister(SetIntensity2HoldingRegister, (ushort) value);
            }
        }

        public int FanSpeed
        {
            set => modbusMaster.BroadcastWriteSingleRegister(FanSpeedHoldingRegister, (ushort) value);
        }

        public bool SwTrigger
        {
            set => modbusMaster.BroadcastWriteSingleCoil(SwTriggerCoil, value);
        }

        public bool FanEnable
        {
            set => modbusMaster.BroadcastWriteSingleCoil(FanEnableCoil, value);
        }

        public TriggerMode TriggerMode
        {
            set
            {
                modbusMaster.BroadcastWriteSingleRegister(InputSettingsHoldingRegister,(ushort) value);
            }
        }

        public IEnumerable<int> ProtocolVersions => ReadInputRegisterFromSegments(ProtocolVersionInputRegister);
        public IEnumerable<int> SoftwareVersions => ReadInputRegisterFromSegments(SoftwareVersionInputRegister);
        public IEnumerable<int> HardwareVersions => ReadInputRegisterFromSegments(HardwareVersionInputRegister);
        public IEnumerable<uint> SerialNumber => ReadUInt32SFromSegments(SnHighInputRegister, SnLowInputRegister);

        public IEnumerable<uint> ProductNumber => ReadUInt32SFromSegments(PnHighInputRegister, PnLowInputRegister);
        public IEnumerable<int> BodyTemperatures => ReadInputRegisterFromSegments(BodyTemperatureInputRegister);
        public IEnumerable<int> BodyMaxTemperatures => ReadInputRegisterFromSegments(BodyMaxTemperatureInputRegister);
        public IEnumerable<int> FanCurrentRpms => ReadInputRegisterFromSegments(FanCurrentRpmInputRegister, "Reading fan rmp failed. Note that reading has been implemented since protocol version 1.01.");

        public IEnumerable<int> LedTemperatures => ReadInputRegisterFromSegments(LedTemperatureInputRegister);
        public IEnumerable<int> LedMaxTemperatures => ReadInputRegisterFromSegments(LedMaxTemperatureInputRegister);
        public IEnumerable<int> LuxValues => ReadInputRegisterFromSegments(LuxMeterValueInputRegister);

        public IEnumerable<double> Amperes1 =>
            ReadInputRegisterFromSegments(Current1InputRegister).Select(x => x / 1000.0);

        public IEnumerable<double> Amperes2 =>
            ReadInputRegisterFromSegments(Current2InputRegister).Select(x => x / 1000.0);

        public IEnumerable<double> Volts1 =>
            ReadInputRegisterFromSegments(Voltage1InputRegister).Select(x => x / 1000.0);

        public IEnumerable<double> Volts2 =>
            ReadInputRegisterFromSegments(Voltage2InputRegister).Select(x => x / 1000.0);

        public IEnumerable<bool> FanEnables => ReadCoils(FanEnableCoil);

        public IEnumerable<int> SetIntensities1 => ReadHoldingRegisterFromSegments(SetIntensity1HoldingRegister);
        public IEnumerable<int> SetIntensities2 => ReadHoldingRegisterFromSegments(SetIntensity2HoldingRegister);


        private IEnumerable<int> ReadHoldingRegisterFromSegments(ushort address)
        {
            return Enumerable.Range(slaveBaseAddress, segmentCount)
                             .Select(index => modbusMaster.ReadHoldingRegisters((byte)index, address, 1)[0])
                             .Select(x => (int)x);
        }

        private IEnumerable<int> ReadInputRegisterFromSegments(ushort address)
        {
            return ReadInputRegisterFromSegments(address, "Input register read error. Check that requested value is supported by the protocol number light is implementing.");
        }

        private IEnumerable<int> ReadInputRegisterFromSegments(ushort address, string exceptionMessage)
        {
            return Enumerable.Range(slaveBaseAddress, segmentCount)
                             .Select(index =>
                             {
                                 try
                                 {
                                     return modbusMaster.ReadInputRegisters((byte) index, address, 1)[0];
                                 }
                                 catch(Exception e)
                                 {
                                     throw new Exception(exceptionMessage, e);
                                 }
                             })
            .Select(x => (int)x);
        }

        private IEnumerable<uint> ReadUInt32SFromSegments(ushort addressHigh, ushort addressLow)
        {
            return Enumerable.Range(slaveBaseAddress, segmentCount)
                .Select(index =>
                {
                    var high = modbusMaster.ReadInputRegisters((byte) index, addressHigh, 1)[0];
                    var low = modbusMaster.ReadInputRegisters((byte) index, addressLow, 1)[0];
                    return (((uint) high) << 16) | low;
                });
        }

        private IEnumerable<bool> ReadCoils(ushort address)
        {
            return Enumerable.Range(slaveBaseAddress, segmentCount)
                .Select(index => modbusMaster.ReadCoils((byte) index, address, 1)[0]);
        }

        private IEnumerable<bool> ReadDiscreteInputsFromSegments(ushort address)
        {
            return Enumerable.Range(slaveBaseAddress, segmentCount)
                .Select(index => modbusMaster.ReadDiscreteInputs((byte) index, address, 1)[0]);
        }
    }
}
