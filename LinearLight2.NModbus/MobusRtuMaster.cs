using System.Threading;
using LinearLight2.NModbus.NModbusExtension;
using NModbus;
using NModbus.IO;

namespace LinearLight2.NModbus
{
    public class ModbusRtuMaster : IModbusMaster
    {
        private global::NModbus.IModbusMaster master;
        private const int MillisecondsDelayBetweenTransmits = 30;
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
                master.WriteSingleCoil(slaveAddress, coilAddress, value);
            }
        }

        public void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value)
        {
            lock (communicatorLock)
            {
                master.WriteSingleRegister(slaveAddress, registerAddress, value);
            }
        }

        public void BroadcastWriteSingleRegister(ushort registerAddress, ushort value)
        {
            lock (communicatorLock)
            {
                master.BroadcastWriteSingleRegister(registerAddress, value);
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
            }
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                return master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
            }
        }

        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                return master.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
            }
        }

        public bool[] ReadDiscreteInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                return master.ReadInputs(slaveAddress, startAddress, numberOfPoints);
            }
        }

        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                return master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
            }
        }

        public void BroadcastWriteSingleCoil(ushort coilAddress, bool value)
        {
            lock (communicatorLock)
            {
                master.BroadcastWriteSingleCoil(coilAddress, value);
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
            }
        }
    }
}