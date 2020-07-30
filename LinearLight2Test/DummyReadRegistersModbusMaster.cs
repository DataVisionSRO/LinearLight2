using System;
using LinearLight2.Modbus;
using NUnit.Framework;

namespace LinearLight2Test
{
    internal class DummyReadRegistersModbusMaster : IModbusMaster
    {
        private readonly byte[] expectedSlaveAddresses;
        private readonly ushort[] expectedStartAddresses;
        private readonly ushort[] expectedLengths;
        private readonly ushort[][] returnValues;
        private int i;

        public DummyReadRegistersModbusMaster(byte[] expectedSlaveAddresses, ushort[] expectedStartAddresses,
                                              ushort[] expectedLengths, ushort[][] returnValues)
        {
            this.expectedSlaveAddresses = expectedSlaveAddresses;
            this.expectedStartAddresses = expectedStartAddresses;
            this.expectedLengths = expectedLengths;
            this.returnValues = returnValues;
        }

        public int MillisecondsDelayBetweenTransmits { get; set; }

        public void BroadcastWriteSingleRegister(ushort registerAddress, ushort value)
        {
            throw new NotImplementedException();
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            Assert.Multiple(() =>
                            {
                                Assert.AreEqual(expectedSlaveAddresses[i], slaveAddress);
                                Assert.AreEqual(expectedStartAddresses[i], startAddress);
                                Assert.AreEqual(expectedLengths[i],numberOfPoints);
                            });
            return returnValues[i++];
        }

        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadDiscreteInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
        }

        public void BroadcastWriteSingleCoil(ushort coilAddress, bool value)
        {
            throw new NotImplementedException();
        }

        public void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value)
        {
            throw new NotImplementedException();
        }

        public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
        {
            throw new NotImplementedException();
        }
    }
}