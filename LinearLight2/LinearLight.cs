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

        public IEnumerable<bool> LightOnFlags => Segments.Select(x => x.LightOnFlag);
        public IEnumerable<bool> HardwareTriggerStatus => Segments.Select(x=>x.HardwareTriggerStatus);
        public IEnumerable<bool> BodyOverheatFlags => Segments.Select(x=>x.BodyOverheatFlag);
        public IEnumerable<bool> LedOverheatFlags => Segments.Select(x=>x.LedOverheatFlag);

        public IEnumerable<int> ProtocolVersions => Segments.Select(x=>x.ProtocolVersion);
        public IEnumerable<int> SoftwareVersions => Segments.Select(x=>x.SoftwareVersion);
        public IEnumerable<int> HardwareVersions => Segments.Select(x=>x.HardwareVersion);
        public IEnumerable<uint> SerialNumber => Segments.Select(x=>x.SerialNumber);

        public IEnumerable<uint> ProductNumber => Segments.Select(x=>x.ProductNumber);
        public IEnumerable<int> BodyTemperatures => Segments.Select(x=>x.BodyTemperature);
        public IEnumerable<int> BodyMaxTemperatures => Segments.Select(x=>x.BodyMaxTemperature);
        public IEnumerable<int> FanCurrentRpms => Segments.Select(x=>x.FanCurrentRpm);

        public IEnumerable<int> LedTemperatures => Segments.Select(x => x.LedTemperature);
        public IEnumerable<int> LedMaxTemperatures => Segments.Select(x => x.LedMaxTemperature);
        public IEnumerable<int> LuxValues => Segments.Select(x=>x.LuxValue);

        public IEnumerable<double> Amperes1 => Segments.Select(x => x.Amperes1);

        public IEnumerable<double> Amperes2 => Segments.Select(x => x.Amperes2);

        public IEnumerable<double> Volts1 => Segments.Select(x => x.Volts1);

        public IEnumerable<double> Volts2 => Segments.Select(x => x.Volts2);

        public IEnumerable<bool> FanEnables => Segments.Select(x => x.FanEnable);

        public IEnumerable<int> SetIntensities1 => Segments.Select(x=>x.SetIntensity1);
        public IEnumerable<int> SetIntensities2 => Segments.Select(x=>x.SetIntensity2);

        public IEnumerable<ConfigurationStatus> ConfigurationRegisters => Segments.Select(x => x.ConfigurationRegister);
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
