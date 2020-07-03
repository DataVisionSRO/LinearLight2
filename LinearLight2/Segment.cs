using System;

namespace LinearLight2
{
    [Obsolete("Will be removed in next version, use SegmentV103 instead.")]
    public class Segment : SegmentV103
    {
        public Segment(IModbusMaster modbusMaster, byte slaveAddress) : base(modbusMaster, slaveAddress)
        {
        }
    }
}