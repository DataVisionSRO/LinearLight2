﻿using System;
using System.Collections.Generic;
using LinearLight2.Modbus;

namespace LinearLight2Test
{
    internal class DummyBroadcastWriteCoilModbusMaster : IModbusMaster
    {
        public int MillisecondsDelayBetweenTransmits { get; set; }

        public void BroadcastWriteSingleRegister(ushort registerAddress, ushort value)
        {
            throw new NotImplementedException();
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            throw new NotImplementedException();
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
            CalledCoilAdresses.Add(coilAddress);
            CalledValues.Add(value);
        }

        public void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value)
        {
            throw new NotImplementedException();
        }

        public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
        {
            throw new NotImplementedException();
        }

        public readonly List<ushort> CalledCoilAdresses = new List<ushort>();
        public readonly List<bool> CalledValues = new List<bool>();
    }
}