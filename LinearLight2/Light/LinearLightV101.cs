using System.Collections.Generic;
using System.Linq;
using LinearLight2.Light.Segment;
using LinearLight2.Light.Settings;
using LinearLight2.Modbus;

namespace LinearLight2.Light
{
    public class LinearLightV101:ILinearLightV101
    {
        private const int CompatibleProtocolVersion = 0x101;
        private readonly IModbusMaster modbusMaster;
        private readonly List<SegmentV101> segments;

        public LinearLightV101(IModbusMaster master, int segmentCount) : this(master, segmentCount, 1)
        {
        }

        public LinearLightV101(IModbusMaster master, int segmentCount, byte startAddr)
        {
            modbusMaster = master;
            segments = new List<SegmentV101>(Enumerable.Range(startAddr, segmentCount)
                .Select(x => new SegmentV101(modbusMaster, (byte) x)));
        }

        public bool IsCompatibleProtocolVersion => Segments.All(x => x.ProtocolVersion == CompatibleProtocolVersion);

        public int MillisecondsBetweenTransmits => 70;
        public IReadOnlyList<ISegmentV101> Segments => segments.AsReadOnly();

        public int Intensity
        {
            set
            {
                modbusMaster.BroadcastWriteSingleRegister(SegmentV101.SetIntensity1HoldingRegister, (ushort) value);
                modbusMaster.BroadcastWriteSingleRegister(SegmentV101.SetIntensity2HoldingRegister, (ushort) value);
            }
        }

        public int FanSpeed
        {
            set => modbusMaster.BroadcastWriteSingleRegister(SegmentV101.FanSpeedHoldingRegister, (ushort) value);
        }

        public bool SwTrigger
        {
            set => modbusMaster.BroadcastWriteSingleCoil(SegmentV101.SwTriggerCoil, value);
        }

        public bool FanEnable
        {
            set => modbusMaster.BroadcastWriteSingleCoil(SegmentV101.FanEnableCoil, value);
        }

        public TriggerMode TriggerMode
        {
            set => modbusMaster.BroadcastWriteSingleRegister(SegmentV101.InputSettingsHoldingRegister, (ushort) value);
        }
    }
}