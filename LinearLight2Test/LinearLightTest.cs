using System;
using System.IO.Ports;
using System.Linq;
using LinearLight2;
using NModbus.IO;
using NUnit.Framework;

namespace LinearLight2Test
{
    [TestFixture]
    [Explicit("Tests require connection to light and manual usage")]
    public class LinearLightTest
    {
        private string comport = "COM28";

        [Test]
        public void ReadTemperatureRegisters()
        {
            var resource = new Communicator(comport);
            var lili = new LinearLight(resource,3);
            foreach (var liliTemperature in lili.Temperatures)
            {
                Console.Out.WriteLine("temperature is " + liliTemperature);
            }
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        public void WriteAndReadHoldingRegister(int number)
        {
            using (var streamResource = new Communicator(comport))
            {   
                   
                var lili = new LinearLight(streamResource,3);
                 
                lili.Intensity = number;
                CollectionAssert.AreEqual(Enumerable.Repeat(number,3), lili.Intensities);
            }
        }
    }

    public class Communicator : IStreamResource
    {
        private SerialPort serial;

        public Communicator(string comport)
        {
            serial = new SerialPort(comport, 38400, Parity.Even);
            serial.Open();
        }
        public void Dispose()
        {
            serial?.Dispose();
        }

        public void DiscardInBuffer()
        {
            serial?.DiscardInBuffer();
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return serial.Read(buffer, offset, count);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            serial.Write(buffer, offset, count);
        }

        public int InfiniteTimeout => -1;

        public int ReadTimeout
        {
            get => serial.ReadTimeout;
            set => serial.ReadTimeout = value;
        }

        public int WriteTimeout
        {
            get => serial.WriteTimeout;
            set => serial.WriteTimeout = value;
        }
    }
}
