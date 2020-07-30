namespace LinearLight2.Modbus
{
    public interface IModbusMaster
    {
        int MillisecondsDelayBetweenTransmits { get; set; }
        void BroadcastWriteSingleRegister(ushort registerAddress, ushort value);
        ushort[] ReadHoldingRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        ushort[] ReadInputRegisters(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        bool[] ReadDiscreteInputs(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        bool[] ReadCoils(byte slaveAddress, ushort startAddress, ushort numberOfPoints);
        void BroadcastWriteSingleCoil(ushort coilAddress, bool value);
        void WriteSingleCoil(byte slaveAddress, ushort coilAddress, bool value);
        void WriteSingleRegister(byte slaveAddress, ushort registerAddress, ushort value);
    }
}
