using System.Collections.Generic;

namespace LinearLight2
{
    public interface ILinearLightV102 : ILinearLightV101
    {
        new IReadOnlyList<ISegmentV102> Segments { get; }
        ConfigurationStatus Configuration { set; }
    }
}