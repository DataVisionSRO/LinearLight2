using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LinearLight2
{
    public class LinearLight
    {

        private readonly IModbusMaster modbusMaster;
        private readonly ushort setIntensity1HoldingRegister = 4000 - 1;
        private readonly ushort setIntensity2HoldingRegister = 4001 - 1;
        private readonly int segmentCount;
        private const byte SlaveBaseAddress = 0x01;
        private const int MillisecondsDelayBetweenTransmits = 70;
        
        public LinearLight(IModbusMaster master, int segmentCount)
        {
            this.segmentCount = segmentCount;
            modbusMaster = master;
        }
        
        public int Intensity
        {
            set
            {
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
                modbusMaster.BroadcastWriteSingleRegister(setIntensity1HoldingRegister, (ushort) value);
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
                modbusMaster.BroadcastWriteSingleRegister(setIntensity2HoldingRegister, (ushort) value);
            }
        }

        public IEnumerable<int> SetIntensities1 => ReadHoldingRegisterFromSegments(setIntensity1HoldingRegister);
        public IEnumerable<int> SetIntensities2 => ReadHoldingRegisterFromSegments(setIntensity2HoldingRegister);


        private IEnumerable<int> ReadHoldingRegisterFromSegments(ushort address)
        {
            foreach (var index in Enumerable.Range(SlaveBaseAddress, segmentCount))
            {
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
                yield return modbusMaster.ReadHoldingRegisters((byte) index, address, 1)[0];
            }
        }
    }
}