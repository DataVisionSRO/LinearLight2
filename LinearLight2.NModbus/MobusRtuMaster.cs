using LinearLight2.NModbus.NModbusExtension;
using NModbus;
using NModbus.IO;

namespace LinearLight2.NModbus
{
    public class ModbusRtuMaster:IModbusMaster
    {
        private global::NModbus.IModbusMaster master;
        public ModbusRtuMaster(IStreamResource streamResource)
        {
            var modbusFactory = new ModbusFactory();

            master = modbusFactory.CreateRtuMaster(streamResource);
            master.Transport.WriteTimeout = 200;
            master.Transport.ReadTimeout = 200;
            master.Transport.Retries = 3;
        }
        public void BroadcastWriteSingleRegister(ushort registerAddress, ushort value)
        {
            master.BroadcastWriteSingleRegister(registerAddress,value);
        }

        public ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints)
        {
            return master.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
        }
    }
}
