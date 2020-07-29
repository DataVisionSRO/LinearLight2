using System.Collections.Generic;

namespace LinearLight2
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