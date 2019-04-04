using System;
using LinearLight2;
using NUnit.Framework;

namespace LinearLight2Test
{
    internal class DummyReadCoilsModbusMaster : IModbusMaster
    {
        private readonly byte[] expectedSlaveAddresses;
        private readonly ushort[] expectedStartAddresses;
        private readonly ushort[] expectedLengths;
        private readonly bool[][] returnValues;
        private int i;

        public DummyReadCoilsModbusMaster(byte[] expectedSlaveAddresses, ushort[] expectedStartAddresses,
                                              ushort[] expectedLengths, bool[][] returnValues)
        {
            this.expectedSlaveAddresses = expectedSlaveAddresses;
            this.expectedStartAddresses = expectedStartAddresses;
            this.expectedLengths = expectedLengths;
            this.returnValues = returnValues;
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
            Assert.Multiple(() =>
                            {
                                Assert.AreEqual(expectedSlaveAddresses[i], slaveAddress);
                                Assert.AreEqual(expectedStartAddresses[i], startAddress);
                                Assert.AreEqual(expectedLengths[i], numberOfPoints);
                            });
            return returnValues[i++]; 
        }

        public void BroadcastWriteSingleCoil(ushort coilAddress, bool value)
        {
            throw new NotImplementedException();
        }
    }
}