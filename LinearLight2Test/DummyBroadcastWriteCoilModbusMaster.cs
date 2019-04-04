using System;
using LinearLight2;
using NUnit.Framework;

namespace LinearLight2Test
{
    internal class DummyBroadcastWriteCoilModbusMaster : IModbusMaster
    {
        private readonly ushort[] setRegister;
        private readonly bool[] values;
        private int i;

        public DummyBroadcastWriteCoilModbusMaster(ushort[] setRegister, bool[] values)
        {
            this.setRegister = setRegister;
            this.values = values;
        }
        public void BroadcastWriteSingleRegister(ushort registerAddress, ushort value)
        {
            throw new NotImplementedException();
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public void BroadcastWriteSingleCoil(ushort coilAddress, bool value)
        {
            Assert.Multiple(() =>
                            {
                                Assert.AreEqual(values[i], value);
                                Assert.AreEqual(setRegister[i++], coilAddress);
                            });

        }
    }
}