using System.Collections.Generic;
using LinearLight2.Light.Segment;
using LinearLight2.Light.Settings;

namespace LinearLight2.Light
{
    public interface ILinearLightV102 : ILinearLightV101
    {
        new IReadOnlyList<ISegmentV102> Segments { get; }
        ConfigurationStatus Configuration { set; }
    }
}