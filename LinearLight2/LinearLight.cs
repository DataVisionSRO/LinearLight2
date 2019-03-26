using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LinearLight2.NModbusExtension;
using NModbus;
using NModbus.IO;

namespace LinearLight2
{
    public class LinearLight
    {

        private readonly IModbusSerialMaster modbusSerialMaster;
        private ushort HoldingRegister = 4000 - 1;
        private readonly int segmentCount;
        private const byte SlaveAddress = 0x01;
        private const int MillisecondsDelayBetweenTransmits = 70;

        public LinearLight(IStreamResource streamResource,int segmentCount)
        {
            var modbusFactory = new ModbusFactory();
            this.segmentCount = segmentCount;
            
            modbusSerialMaster = modbusFactory.CreateRtuMaster(streamResource);
            modbusSerialMaster.Transport.WriteTimeout = 200;
            modbusSerialMaster.Transport.ReadTimeout = 200;
            modbusSerialMaster.Transport.Retries = 3;
        }

        public List<int> Temperatures => throw new NotImplementedException();

        public int Intensity
        {
            set
            {
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
                modbusSerialMaster.BroadcastWriteSingleRegister(HoldingRegister, (ushort) value);
            }
        }

        public IEnumerable<int> Intensities
        {
            get
            {
                foreach (var index in Enumerable.Range(SlaveAddress, segmentCount))
                {
                    Thread.Sleep(MillisecondsDelayBetweenTransmits);
                    yield return modbusSerialMaster.ReadHoldingRegisters((byte) index, HoldingRegister, 1)[0];
                }
            }
        }
    }
}