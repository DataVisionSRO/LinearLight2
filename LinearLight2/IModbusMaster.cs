namespace LinearLight2
{
    public interface IModbusMaster
    {
        void BroadcastWriteSingleRegister(ushort registerAddress, ushort value);
        ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        bool[] ReadDiscreteInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        void BroadcastWriteSingleCoil(ushort coilAddress, bool value);
    }
}
