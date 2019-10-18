using System.Threading;
using LinearLight2.NModbus.NModbusExtension;
using NModbus;
using NModbus.IO;

namespace LinearLight2.NModbus
{
    public class ModbusRtuMaster:IModbusMaster
    {
        private global::NModbus.IModbusMaster master;
        private const int MillisecondsDelayBetweenTransmits = 70;
        private readonly object communicatorLock = new object();

        public ModbusRtuMaster(IStreamResource streamResource)
        {
            var modbusFactory = new ModbusFactory();

            master = modbusFactory.CreateRtuMaster(streamResource);
            master.Transport.WriteTimeout = 200;
            master.Transport.ReadTimeout = 200;
            master.Transport.Retries = 3;
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
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
                master.BroadcastWriteSingleRegister(registerAddress, value);
            }
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
                return master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
            }
        }

        public ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
                return master.ReadInputRegisters(slaveAddress, startAddress, numberOfPoints);
            }
        }

        public bool[] ReadDiscreteInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
                return master.ReadInputs(slaveAddress, startAddress, numberOfPoints);
            }
        }

        public bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
                return master.ReadCoils(slaveAddress, startAddress, numberOfPoints);
            }
        }

        public void BroadcastWriteSingleCoil(ushort coilAddress, bool value)
        {
            lock (communicatorLock)
            {
                Thread.Sleep(MillisecondsDelayBetweenTransmits);
                master.BroadcastWriteSingleCoil(coilAddress, value);
            }
        }
    }
}
