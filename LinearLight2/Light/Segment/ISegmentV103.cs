using LinearLight2.Light.Settings;

namespace LinearLight2.Light.Segment
{
    public interface ISegmentV103 : ISegmentV102
    {
        int FanSetSpeed { get; }
        int FanAutoMinSpeed { get; }
        int FanAutoMaxSpeed { get; }
        int FanAutoMinTemp { get; }
        int FanAutoMaxTemp { get; }
        FanMode FanMode { get; set; }
    }
}