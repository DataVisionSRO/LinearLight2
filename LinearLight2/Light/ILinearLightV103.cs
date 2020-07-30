using System.Collections.Generic;
using LinearLight2.Light.Segment;
using LinearLight2.Light.Settings;

namespace LinearLight2.Light
{
    public interface ILinearLightV103 : ILinearLightV102
    {
        new IReadOnlyList<ISegmentV103> Segments { get; }
        FanMode FanMode { set; }
    }
}