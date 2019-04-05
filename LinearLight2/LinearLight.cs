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
        private readonly ushort swTriggerCoil = 1000 - 1;
        private readonly ushort fanEnableCoil = 1001 - 1;
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

        public IEnumerable<bool> FanEnables
        {
            get => ReadCoils(fanEnableCoil);
        }

        private IEnumerable<bool> ReadCoils(ushort address)
        {
            return Enumerable.Range(SlaveBaseAddress, segmentCount)
                             .Select(index => modbusMaster.ReadCoils((byte) index, address, 1)[0]);
        }

        public IEnumerable<int> SetIntensities1 => ReadHoldingRegisterFromSegments(setIntensity1HoldingRegister);
        public IEnumerable<int> SetIntensities2 => ReadHoldingRegisterFromSegments(setIntensity2HoldingRegister);


        private IEnumerable<int> ReadHoldingRegisterFromSegments(ushort address)
        {
            return Enumerable.Range(SlaveBaseAddress, segmentCount)
                             .Select(index => modbusMaster.ReadHoldingRegisters((byte) index, address, 1)[0])
                             .Select(dummy => (int) dummy);
        }
    }
}