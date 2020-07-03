using System;

namespace LinearLight2
{
    [Obsolete("Will be removed in next version. Use LinearLightV103 instead.")]
    public class LinearLight : LinearLightV103
    {
        public LinearLight(IModbusMaster master, int segmentCount) : base(master, segmentCount)
        {
        }

        public LinearLight(IModbusMaster master, int segmentCount, byte startAddr) : base(master, segmentCount,
            startAddr)
        {
        }
    }
}