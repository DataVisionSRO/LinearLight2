using System.Linq;
using LinearLight2.NModbus.NModbusExtension;
using NUnit.Framework;

namespace LinearLight2Test.LinearLight2.NModbus
{
    [TestFixture]
    public class BroadcastWriteRegisterMessageTest
    {
        [Test]
        public void BroadcastWriteRegisterMessageFunctionCodeTest()
        {
            var message = new BroadcastWriteRegisterMessage(10,20);
            Assert.AreEqual(6, message.FunctionCode);
        }

        [Test]
        public void BroadcastWriteRegisterMessageSlaveAddrTest()
        {
            var message = new BroadcastWriteRegisterMessage(10, 20);
            Assert.AreEqual(0, message.SlaveAddress);
        }

        [Test]
        public void BroadcastWriteRegisterMessagePduTest()
        {
            ushort registerAddr = 10;
            ushort registerValue = 20;
            var message = new BroadcastWriteRegisterMessage(registerAddr, registerValue);
            var expectedPdu = new[]
                              {
                                  message.FunctionCode,
                                  (byte) (registerAddr >> 8), (byte) (registerAddr & 0xFF),
                                  (byte) (registerValue >> 8), (byte) (registerValue & 0xFF),
                              };
            Assert.AreEqual(expectedPdu, message.ProtocolDataUnit);
        }

        [Test]
        public void BroadcastWriteRegisterMessageMessageFrameTest()
        {
            ushort registerAddr = 10;
            ushort registerValue = 20;
            var message = new BroadcastWriteRegisterMessage(registerAddr, registerValue);

            var messageFrame = message.MessageFrame;
            Assert.AreEqual(message.SlaveAddress, messageFrame[0]);
            Assert.AreEqual(message.ProtocolDataUnit, messageFrame.Skip(1));
        }
    }
}
