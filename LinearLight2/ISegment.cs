namespace LinearLight2
{
    public interface ISegment
    {
        bool LightOnFlag { get; }
        bool HardwareTriggerStatus { get; }
        bool BodyOverheatFlag { get; }
        bool LedOverheatFlag { get; }
        bool FanEnable { get; set; }
        bool SwTrigger { get; set; }
        int ProtocolVersion { get; }
        int SoftwareVersion { get; }
        int HardwareVersion { get; }
        string SerialNumber { get; }
        string ProductNumber { get; }
        int BodyTemperature { get; }
        int BodyMaxTemperature { get; }
        int FanCurrentRpm { get; }
        int FanSetSpeed { get; }
        int FanAutoMinSpeed { get; }
        int FanAutoMaxSpeed { get; }
        int FanAutoMinTemp { get; }
        int FanAutoMaxTemp { get; }
        int LedTemperature { get; }
        int LedMaxTemperature { get; }
        int LuxValue { get; }
        double Amperes1 { get; }
        double Amperes2 { get; }
        double Volts1 { get; }
        double Volts2 { get; }
        TriggerMode TriggerMode { get; set; }
        int SetFanSpeed { get; set; }
        FanMode FanMode { get; set; }
        int SetIntensity1 { get; set; }
        int SetIntensity2 { get; set; }
        ConfigurationStatus ConfigurationRegister { get; set; }
    }
}