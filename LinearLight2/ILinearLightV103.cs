using System.Collections.Generic;

namespace LinearLight2
{
    public interface ILinearLightV103 : ILinearLightV102
    {
        new IReadOnlyList<ISegmentV103> Segments { get; }
        FanMode FanMode { set; }
    }
}