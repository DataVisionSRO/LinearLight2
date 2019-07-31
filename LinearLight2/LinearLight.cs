using System.Collections.Generic;
using System.Linq;

namespace LinearLight2
{
    public class LinearLight
    {

        private readonly IModbusMaster modbusMaster;
        private readonly ushort setIntensity1HoldingRegister = 4000 - 1;
        private readonly ushort setIntensity2HoldingRegister = 4001 - 1;
        private readonly ushort fanSpeedHoldingRegister = 4002 - 1;
        private readonly ushort swTriggerCoil = 2000 - 1;
        private readonly ushort fanEnableCoil = 2001 - 1;
        private readonly ushort bodyTemperatureInputRegister = 3101 - 1;
        private readonly ushort ledTemperatureInputRegister = 3201 - 1;
        private readonly ushort voltage1InputRegister = 3211 - 1;
        private readonly ushort current1InputRegister = 3212 - 1;
        private readonly ushort voltage2InputRegister = 3221 - 1;
        private readonly ushort current2InputRegister = 3222 - 1;
        private readonly int segmentCount;
        private const byte SlaveBaseAddress = 0x01;
        
        public LinearLight(IModbusMaster master, int segmentCount)
        {
            this.segmentCount = segmentCount;
            modbusMaster = master;
        }
        
        public int Intensity
        {
            set
            {
                modbusMaster.BroadcastWriteSingleRegister(setIntensity1HoldingRegister, (ushort) value);
                modbusMaster.BroadcastWriteSingleRegister(setIntensity2HoldingRegister, (ushort) value);
            }
        }

        public int FanSpeed
        {
            set => modbusMaster.BroadcastWriteSingleRegister(fanSpeedHoldingRegister, (ushort) value);
        }

        public bool SwTrigger
        {
            set => modbusMaster.BroadcastWriteSingleCoil(swTriggerCoil, value);
        }

        public bool FanEnable
        {
            set => modbusMaster.BroadcastWriteSingleCoil(fanEnableCoil, value);
        }

        public IEnumerable<int> BodyTemperatures => ReadInputRegisterFromSegments(bodyTemperatureInputRegister);

        public IEnumerable<int> LedTemperatures => ReadInputRegisterFromSegments(ledTemperatureInputRegister);

        public IEnumerable<double> Amperes1 =>
            ReadInputRegisterFromSegments(current1InputRegister).Select(x => x / 1000.0);

        public IEnumerable<double> Amperes2 =>
            ReadInputRegisterFromSegments(current2InputRegister).Select(x => x / 1000.0);

        public IEnumerable<double> Volts1 =>
            ReadInputRegisterFromSegments(voltage1InputRegister).Select(x => x / 1000.0);

        public IEnumerable<double> Volts2 =>
            ReadInputRegisterFromSegments(voltage2InputRegister).Select(x => x / 1000.0);

        public IEnumerable<bool> FanEnables => ReadCoils(fanEnableCoil);

        public IEnumerable<int> SetIntensities1 => ReadHoldingRegisterFromSegments(setIntensity1HoldingRegister);
        public IEnumerable<int> SetIntensities2 => ReadHoldingRegisterFromSegments(setIntensity2HoldingRegister);


        private IEnumerable<int> ReadHoldingRegisterFromSegments(ushort address)
        {
            return Enumerable.Range(SlaveBaseAddress, segmentCount)
                             .Select(index => modbusMaster.ReadHoldingRegisters((byte)index, address, 1)[0])
                             .Select(x => (int)x);
        }
        private IEnumerable<int> ReadInputRegisterFromSegments(ushort address)
        {
            return Enumerable.Range(SlaveBaseAddress, segmentCount)
                             .Select(index => modbusMaster.ReadInputRegisters((byte)index, address, 1)[0])
                             .Select(x => (int)x);
        }
        
        private IEnumerable<bool> ReadCoils(ushort address)
        {
            return Enumerable.Range(SlaveBaseAddress, segmentCount)
                .Select(index => modbusMaster.ReadCoils((byte) index, address, 1)[0]);
        }
    }
}
