using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NModbus.IO;

namespace LinearLight2
{
    public class LinearLight
    {

        private readonly IModbusMaster modbusMaster;
        private ushort HoldingRegister = 4000 - 1;
        private readonly int segmentCount;
        private const byte SlaveAddress = 0x01;
        private const int MillisecondsDelayBetweenTransmits = 70;

        [Obsolete("Reference to NModbus will be removed in next commit.")]
        public LinearLight(IStreamResource streamResource,int segmentCount)
        {
            this.segmentCount = segmentCount;
            modbusMaster = new ModbusRtuMaster(streamResource);
        }

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
                modbusMaster.BroadcastWriteSingleRegister(HoldingRegister, (ushort) value);
            }
        }

        public IEnumerable<int> Intensities
        {
            get
            {
                foreach (var index in Enumerable.Range(SlaveAddress, segmentCount))
                {
                    Thread.Sleep(MillisecondsDelayBetweenTransmits);
                    yield return modbusMaster.ReadHoldingRegisters((byte) index, HoldingRegister, 1)[0];
                }
            }
        }

        public int[] Temperatures => throw new NotImplementedException();
    }
}