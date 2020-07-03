﻿using System.Collections.Generic;
using System.Linq;

namespace LinearLight2
{
    public class LinearLightV103 : LinearLightV102, ILinearLightV103
    {
        private const int CompatibleProtocolVersion = 0x103;
        private readonly IModbusMaster modbusMaster;
        private readonly List<SegmentV103> segments;

        public LinearLightV103(IModbusMaster master, int segmentCount) : this(master, segmentCount, 1)
        {
        }

        public LinearLightV103(IModbusMaster master, int segmentCount, byte startAddr) : base(master, segmentCount,
            startAddr)
        {
            modbusMaster = master;
            segments = new List<SegmentV103>(Enumerable.Range(startAddr, segmentCount)
                .Select(x => new SegmentV103(modbusMaster, (byte) x)));
        }

        public new bool IsCompatibleProtocolVersion =>
            Segments.All(x => x.ProtocolVersion == CompatibleProtocolVersion);

        public new int MillisecondsBetweenTransmits => 5;
        public new IReadOnlyList<ISegmentV103> Segments => segments.AsReadOnly();

        IReadOnlyList<ISegmentV101> ILinearLightV101.Segments => Segments;

        IReadOnlyList<ISegmentV102> ILinearLightV102.Segments => Segments;

        public FanMode FanMode
        {
            set => modbusMaster.BroadcastWriteSingleRegister(SegmentV103.FanModeHoldingRegister, (ushort) value);
        }
    }
}