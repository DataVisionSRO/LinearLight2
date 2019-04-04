using System.Linq;
using LinearLight2.NModbus.NModbusExtension;
using NUnit.Framework;

namespace LinearLight2Test.LinearLight2.NModbus
{
    [TestFixture]
    public class BroadcastWriteCoilMessageTest
    {
        [Test]
        public void BroadcastWriteCoilMessageFunctionCodeTest()
        {
            var message = new BroadcastWriteCoilMessage(10,true);
            Assert.AreEqual(5, message.FunctionCode);
        }

        [Test]
        public void BroadcastWriteCoilMessageSlaveAddrTest()
        {
            var message = new BroadcastWriteCoilMessage(10, false);
            Assert.AreEqual(0, message.SlaveAddress);
        }

        [Test]
        public void BroadcastWriteTrueCoilMessagePduTest()
        {
            ushort CoilAddr = 10;
            var message = new BroadcastWriteCoilMessage(CoilAddr, true);
            var expectedPdu = new[]
                              {
                                  message.FunctionCode,
                                  (byte) (CoilAddr >> 8), (byte) (CoilAddr & 0xFF),
                                  (byte) 0xFF, (byte) 0x00
                              };
            Assert.AreEqual(expectedPdu, message.ProtocolDataUnit);
        }

        [Test]
        public void BroadcastWriteFalseCoilMessagePduTest()
        {
            ushort CoilAddr = 10;
            var message = new BroadcastWriteCoilMessage(CoilAddr, false);
            var expectedPdu = new[]
                              {
                                  message.FunctionCode,
                                  (byte) (CoilAddr >> 8), (byte) (CoilAddr & 0xFF),
                                  (byte) 0x00, (byte) 0x00
                              };
            Assert.AreEqual(expectedPdu, message.ProtocolDataUnit);
        }

        [Test]
        public void BroadcastWriteCoilMessageMessageFrameTest()
        {
            ushort CoilAddr = 10;
            var message = new BroadcastWriteCoilMessage(CoilAddr, false);

            var messageFrame = message.MessageFrame;
            Assert.AreEqual(message.SlaveAddress, messageFrame[0]);
            Assert.AreEqual(message.ProtocolDataUnit, messageFrame.Skip(1));
        }
    }
}
