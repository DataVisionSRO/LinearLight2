using LinearLight2.Light.Settings;
using LinearLight2.Modbus;

namespace LinearLight2.Light.Segment
{
    public class SegmentV102:SegmentV101,ISegmentV102
    {
        public const ushort ConfigurationHoldingRegister = 4090 - 1;

        public SegmentV102(IModbusMaster modbusMaster, byte slaveAddress) : base(modbusMaster, slaveAddress)
        {
        }

        public ConfigurationStatus ConfigurationRegister
        {
            get => (ConfigurationStatus) ReadHoldingRegisterFromSegment(ConfigurationHoldingRegister);
            set => WriteSingleRegister(ConfigurationHoldingRegister, (ushort) value);
        }
    }
}