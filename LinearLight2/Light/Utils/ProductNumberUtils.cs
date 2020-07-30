using System;

namespace LinearLight2.Light.Utils
{
    public static class ProductNumberUtils
    {
        public static int GetSegmentCount(string productNumber)
        {
            var (segmentCount, family) = Parse(productNumber);

            AssureSupportedFamily(family);
            return segmentCount;
        }

        private static (int segmentCount, int family) Parse(string productNumber)
        {
            var number = Convert.ToUInt32(productNumber, 16);
            var bytes = BitConverter.GetBytes(number);
            return (bytes[0], bytes[3]);
        }

        private static void AssureSupportedFamily(int family)
        {
            if (family == 4 || family == 5)
                return;

            throw new NotSupportedException($"Family {family} is not supported.");
        }
    }
}