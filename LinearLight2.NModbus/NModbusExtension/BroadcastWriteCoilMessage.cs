﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NModbus;
using NModbus.Data;

namespace LinearLight2.NModbus.NModbusExtension
{
    public class BroadcastWriteCoilMessage : IModbusMessage
    {
        private readonly ushort? startAddress;
        private readonly IModbusMessageDataCollection data;

        public BroadcastWriteCoilMessage(ushort addr, bool value)
        {
            startAddress = addr;
            data = new SingleModbusRegister(value ? (ushort) 0xFF00 : (ushort) 0x0000);
        }
        public void Initialize(byte[] frame)
        {
            throw new NotImplementedException();
        }

        public byte FunctionCode
        {
            get => 5;
            set => throw new NotImplementedException();
        }

        public byte SlaveAddress
        {
            get => 0;
            set => throw new NotImplementedException();
        }

        public byte[] MessageFrame {
            get
            {
                var pdu = ProtocolDataUnit;
                var frame = new MemoryStream(1 + pdu.Length);

                frame.WriteByte(SlaveAddress);
                frame.Write(pdu, 0, pdu.Length);

                return frame.ToArray();
            }
        }
        public byte[] ProtocolDataUnit
        {
            get
            {
                var pdu = new List<byte> {FunctionCode};


                if (startAddress.HasValue)
                {
                    pdu.AddRange(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)startAddress.Value)));
                }

                if (data != null)
                {
                    pdu.AddRange(data.NetworkBytes);
                }

                return pdu.ToArray();
            }
        }
        public ushort TransactionId { get; set; }
    }
}