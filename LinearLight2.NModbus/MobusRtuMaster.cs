using System;
using System.Threading;
using LinearLight2.NModbus.NModbusExtension;
using NModbus;
using NModbus.IO;

namespace LinearLight2.NModbus
{
    public class ModbusRtuMaster : IModbusMaster
    {
        private global::NModbus.IModbusMaster master;
        private int millisecondsBetweenTransmits = 5;

        public int MillisecondsDelayBetweenTransmits
        {
            get
            {
                lock (communicatorLock)
                {
                    return millisecondsBetweenTransmits;
                }
            }
            set
            {
                if (value > 0)
                {
                    lock (communicatorLock)
                    {
                        millisecondsBetweenTransmits = value;
                    }
                }
                else
                    throw new ArgumentOutOfRangeException(nameof(value),
                        $"Delay between transmits must be greater than zero.");
            }
        }

        private readonly object communicatorLock = new object();

        public ModbusRtuMaster(IStreamResource streamResource) : this(streamResource, 200, 200, 3)
        {
        }

        public ModbusRtuMaster(IStreamResource streamResource, int writeTimeout, int readTimeout, int retries)
        {
            var modbusFactory = new ModbusFactory();

            master = modbusFactory.CreateRtuMaster(streamResource);
            master.Transport.WriteTimeout = writeTimeout;
            master.Transport.ReadTimeout = readTimeout;
            master.Transport.Retries = retries;
        }

        public void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(millisecondsBetweenTransmits);
                master.WriteSingleCoil(slaveAddress, coilAddress, value);
            }
        }

        public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(millisecondsBetweenTransmits);
                master.WriteSingleRegister(slaveAddress, registerAddress, value);
            }
        }

        public void BroadcastWriteSingleRegister(ushort registerAddress, ushort value)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(millisecondsBetweenTransmits);
                master.BroadcastWriteSingleRegister(registerAddress, value);
            }
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(millisecondsBetweenTransmits);
                var ret = master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                return ret;
            }
        }

        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(millisecondsBetweenTransmits);
                var ret = master.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
                return ret;
            }
        }

        public bool[] ReadDiscreteInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(millisecondsBetweenTransmits);
                var ret = master.ReadInputs(slaveAddress, startAddress, numberOfPoints);
                return ret;
            }
        }

        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(millisecondsBetweenTransmits);
                var ret = master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
                return ret;
            }
        }

        public void BroadcastWriteSingleCoil(ushort coilAddress, bool value)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(millisecondsBetweenTransmits);
                master.BroadcastWriteSingleCoil(coilAddress, value);
            }
        }
    }
}