using System.Collections.Generic;
using LinearLight2.Light.Segment;
using LinearLight2.Light.Settings;

namespace LinearLight2.Light
{
    public interface ILinearLightV101:ILinearLight
    {
        IReadOnlyList<ISegmentV101> Segments { get; }

        int Intensity { set; }

        int FanSpeed { set; }

        bool SwTrigger { set; }

        bool FanEnable { set; }

        TriggerMode TriggerMode { set; }
    }
}