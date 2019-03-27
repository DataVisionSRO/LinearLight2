using System.IO.Ports;
using NModbus.IO;

namespace LinearLight2Test
{
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