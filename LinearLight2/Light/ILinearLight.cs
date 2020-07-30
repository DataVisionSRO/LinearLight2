namespace LinearLight2.Light
{
    public interface ILinearLight
    {
        int MillisecondsBetweenTransmits { get; }
        bool HasCompatibleProtocolVersion { get; }
    }
}