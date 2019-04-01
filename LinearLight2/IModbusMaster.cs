namespace LinearLight2
{
    public interface IModbusMaster
    {
        void BroadcastWriteSingleRegister(ushort registerAddress, ushort value);
        ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
    }
}
