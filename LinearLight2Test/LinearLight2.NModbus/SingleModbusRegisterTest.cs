using LinearLight2.NModbus.NModbusExtension;
using NUnit.Framework;

namespace LinearLight2Test.LinearLight2.NModbus
{
    [TestFixture]
    public class SingleModbusRegisterTest
    {
        [Test]
        public void SingleModbusRegisterByteCountTest()
        {
            var reg = new SingleModbusRegister(10);
            Assert.AreEqual(2, reg.ByteCount);
        }

        [Test]
        public void SingleModbusRegisterNetworkBytesTest()
        {
            ushort value = 10;
            var reg = new SingleModbusRegister(value);
            // ReSharper disable once ShiftExpressionRightOperandNotEqualRealCount
            var expectedBytes = new[] {(byte) (value >> 0xFF), (byte) (value & 0xFF)};
            Assert.AreEqual(expectedBytes, reg.NetworkBytes);
        }

    }
}
