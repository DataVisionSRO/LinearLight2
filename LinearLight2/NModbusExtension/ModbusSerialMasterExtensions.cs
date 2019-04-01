namespace LinearLight2.NModbusExtension
{
    public static class ModbusSerialMasterExtensions
    {
        public static void BroadcastWriteSingleRegister(this NModbus.IModbusMaster master, ushort addr, ushort value)
        {
            master.Transport.Write(new BroadcastWriteRegisterMessage(addr, value));
        }
    }
}
