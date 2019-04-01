using System;
using LinearLight2;
using NUnit.Framework;

namespace LinearLight2Test
{
    class DummyBroadcastWriteModbusMaster : IModbusMaster
    {
        private readonly ushort setRegister;
        private readonly ushort expectedIntensity;

        public DummyBroadcastWriteModbusMaster(ushort setRegister, ushort expectedIntensity)
        {
            this.setRegister = setRegister;
            this.expectedIntensity = expectedIntensity;
        }
        public void BroadcastWriteSingleRegister(ushort registerAddress, ushort value)
        {
            Assert.Multiple(() =>
                            {
                                Assert.AreEqual(expectedIntensity, value);
                                Assert.AreEqual(setRegister, registerAddress);
                            });
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }
    }
}