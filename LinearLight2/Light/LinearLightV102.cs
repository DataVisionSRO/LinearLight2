using System.Collections.Generic;
using System.Linq;
using LinearLight2.Light.Segment;
using LinearLight2.Light.Settings;
using LinearLight2.Modbus;

namespace LinearLight2.Light
{
    public class LinearLightV102 : LinearLightV101, ILinearLightV102
    {
        private const int CompatibleProtocolVersion = 0x102;
        private readonly IModbusMaster modbusMaster;
        private readonly List<SegmentV102> segments;

        public LinearLightV102(IModbusMaster master, int segmentCount) : this(master, segmentCount, 1)
        {
        }

        public LinearLightV102(IModbusMaster master, int segmentCount, byte startAddr) : base(master, segmentCount,startAddr)
        {
            modbusMaster = master;
            segments = new List<SegmentV102>(Enumerable.Range(startAddr, segmentCount)
                .Select(x => new SegmentV102(modbusMaster, (byte) x)));
        }
        public new bool HasCompatibleProtocolVersion => Segments.All(x => x.ProtocolVersion == CompatibleProtocolVersion);
        public new IReadOnlyList<ISegmentV102> Segments => segments.AsReadOnly();

        IReadOnlyList<ISegmentV101> ILinearLightV101.Segments => Segments;

        public ConfigurationStatus Configuration
        {
            set => modbusMaster.BroadcastWriteSingleRegister(SegmentV102.ConfigurationHoldingRegister, (ushort) value);
        }
    }
}