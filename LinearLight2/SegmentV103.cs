using System;

namespace LinearLight2
{
    public class SegmentV103 : SegmentV102, ISegmentV103
    {
        public const ushort FanModeHoldingRegister = 4110 - 1;
        public const ushort FanSetSpeedInputRegister = 3110 - 1;
        public const ushort FanAutoMinSpeedInputRegister = 3111 - 1;
        public const ushort FanAutoMaxSpeedInputRegister = 3112 - 1;
        public const ushort FanAutoMinTempInputRegister = 3113 - 1;
        public const ushort FanAutoMaxTempInputRegister = 3114 - 1;


        public SegmentV103(IModbusMaster modbusMaster, byte slaveAddress):base(modbusMaster,slaveAddress)
        {
        }

        public int FanSetSpeed => ReadInputRegisterFromSegment(FanSetSpeedInputRegister);
        public int FanAutoMinSpeed => ReadInputRegisterFromSegment(FanAutoMinSpeedInputRegister);
        public int FanAutoMaxSpeed => ReadInputRegisterFromSegment(FanAutoMaxSpeedInputRegister);
        public int FanAutoMinTemp => ReadInputRegisterFromSegment(FanAutoMinTempInputRegister);
        public int FanAutoMaxTemp => ReadInputRegisterFromSegment(FanAutoMaxTempInputRegister);

        public FanMode FanMode
        {
            get => (FanMode) ReadHoldingRegisterFromSegment(FanModeHoldingRegister);
            set => WriteSingleRegister(FanModeHoldingRegister, (ushort) value);
        }
    }
}