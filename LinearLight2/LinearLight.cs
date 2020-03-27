using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace LinearLight2
{
    public class LinearLight
    {

        private readonly IModbusMaster modbusMaster;
        private readonly List<Segment> segments;

        public LinearLight(IModbusMaster master, int segmentCount) : this(master, segmentCount, 1)
        {

        }
        public LinearLight(IModbusMaster master, int segmentCount, byte startAddr)
        {
            modbusMaster = master;
            segments = new List<Segment>(Enumerable.Range(startAddr,segmentCount).Select(x=>new Segment(modbusMaster,(byte)x)));
        }



        public IReadOnlyList<ISegment> Segments => segments.AsReadOnly();

        public int Intensity
        {
            set
            {
                modbusMaster.BroadcastWriteSingleRegister(Segment.SetIntensity1HoldingRegister, (ushort) value);
                modbusMaster.BroadcastWriteSingleRegister(Segment.SetIntensity2HoldingRegister, (ushort) value);
            }
        }

        public int FanSpeed
        {
            set => modbusMaster.BroadcastWriteSingleRegister(Segment.FanSpeedHoldingRegister, (ushort) value);
        }

        public bool SwTrigger
        {
            set => modbusMaster.BroadcastWriteSingleCoil(Segment.SwTriggerCoil, value);
        }

        public bool FanEnable
        {
            set => modbusMaster.BroadcastWriteSingleCoil(Segment.FanEnableCoil, value);
        }

        public TriggerMode TriggerMode
        {
            set => modbusMaster.BroadcastWriteSingleRegister(Segment.InputSettingsHoldingRegister,(ushort) value);
        }

        public ConfigurationStatus Configuration
        {
            set => modbusMaster.BroadcastWriteSingleRegister(Segment.ConfigurationHoldingRegister, (ushort) value);
        }

        public int FanMode
        {
            set => modbusMaster.BroadcastWriteSingleRegister(Segment.FanModeHoldingRegister, (ushort) value);
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum ConfigurationStatus
    {
        OK = 0,
        FAIL = 1,
        Saving = 0x5AFE,
        LoadingDefault = 0xDEFA,
    }
}
