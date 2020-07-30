using LinearLight2.Light.Settings;

namespace LinearLight2.Light.Segment
{
    public interface ISegmentV102 : ISegmentV101
    {
        ConfigurationStatus ConfigurationRegister { get; set; }
    }
}