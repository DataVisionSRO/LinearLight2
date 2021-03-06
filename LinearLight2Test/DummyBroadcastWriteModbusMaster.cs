﻿using System;
using System.Collections.Generic;
using LinearLight2.Modbus;

namespace LinearLight2Test
{
    internal class DummyBroadcastWriteModbusMaster : IModbusMaster
    {
        public int MillisecondsDelayBetweenTransmits { get; set; }

        public void BroadcastWriteSingleRegister(ushort registerAddress, ushort value)
        {
            calledRegisterAdresses.Add(registerAddress);
            calledValues.Add(value);
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

        private readonly List<ushort> calledRegisterAdresses = new List<ushort>();
        private readonly List<ushort> calledValues = new List<ushort>();

        public IReadOnlyList<ushort> CalledRegisterAdresses => calledRegisterAdresses.AsReadOnly();
        public IReadOnlyList<ushort> CalledValues => calledValues.AsReadOnly();
    }
}