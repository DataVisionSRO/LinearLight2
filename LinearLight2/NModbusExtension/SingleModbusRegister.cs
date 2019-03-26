using System;
using System.IO;
using System.Net;
using NModbus.Data;

namespace LinearLight2.NModbusExtension
{
    internal class SingleModbusRegister : IModbusMessageDataCollection
    {
        private readonly ushort value;

        public SingleModbusRegister(ushort value)
        {
            this.value = value;
        }

        public byte[] NetworkBytes {
            get
            {
                var bytes = new MemoryStream(ByteCount);

                var b = BitConverter.GetBytes((ushort)IPAddress.HostToNetworkOrder((short)value));
                bytes.Write(b, 0, b.Length);
               
                return bytes.ToArray();
            }
        }
        public byte ByteCount => 2;
    }
}