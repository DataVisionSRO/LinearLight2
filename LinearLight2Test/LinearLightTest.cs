using System.Linq;
using LinearLight2;
using NUnit.Framework;

namespace LinearLight2Test
{
    [TestFixture]
    public class LinearLightTest
    {
        [Test]
        public void SetIntensityTest()
        {
            ushort expectedIntensity = 15;
            var master = new DummyBroadcastWriteModbusMaster(4000-1, expectedIntensity);
            var linearLight = new LinearLight(master, 0);
            linearLight.Intensity = expectedIntensity;
        }

        [Test]
        public void ReadIntensitiesTest()
        {
            var addresses = new byte[] {1, 2, 3};
            var startAddresses = new ushort[] {4000 - 1, 4000 - 1, 4000 - 1,};
            var lengths = new ushort[] {1, 1, 1};
            var intensities = new ushort[] {30, 54, 15};
            var returnVals = intensities.Select(x => new[] {x}).ToArray();

            var master = new DummyReadRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master,addresses.Length);
            CollectionAssert.AreEqual(intensities, lili.Intensities);
        }

        
    }
}
