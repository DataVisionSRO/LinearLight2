using System;
using NUnit.Framework;

namespace LinearLight2Test.LinearLight2
{
    [TestFixture]
    public class ProductNumberUtilsTest
    {
        [TestCase("04032008", 8)]
        public void TestGetSegmentCount(string productNumber, int expectedSegmentCount)
        {
            var actualSegmentCount = global::LinearLight2.ProductNumberUtils.GetSegmentCount(productNumber);
            Assert.AreEqual(expectedSegmentCount, actualSegmentCount);
        }

        [TestCase("3032008")]
        public void TestGetSegmentCount_UnsupportedFamily(string productNumber)
        {
            Assert.Throws<NotSupportedException>(() =>
                global::LinearLight2.ProductNumberUtils.GetSegmentCount(productNumber));
        }
    }
}
