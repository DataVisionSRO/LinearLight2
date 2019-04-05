using System.Linq;
using LinearLight2;
using NUnit.Framework;

namespace LinearLight2Test.LinearLight2
{
    [TestFixture]
    public class LinearLightTest
    {
        [Test]
        public void SetIntensityTest()
        {
            ushort expectedIntensity = 15;
            var registers = new ushort[] { 4000 - 1, 4001 - 1 };
            var values = new[] { expectedIntensity, expectedIntensity };

            var master = new DummyBroadcastWriteModbusMaster();
            var linearLight = new LinearLight(master, 0);
            linearLight.Intensity = expectedIntensity;

            Assert.AreEqual(registers, master.CalledRegisterAdresses);
            Assert.AreEqual(values, master.CalledValues);
        }

        [Test]
        public void SetFanSpeedTest()
        {
            ushort setSpeed = 50;
            var registers = new ushort[] { 4002 - 1 };
            var values = new[] { setSpeed };
            var master = new DummyBroadcastWriteModbusMaster();
            var linearLight = new LinearLight(master, 0);
            linearLight.FanSpeed = setSpeed;

            Assert.AreEqual(registers, master.CalledRegisterAdresses);
            Assert.AreEqual(values, master.CalledValues);
        }

        [Test]
        public void ReadIntensities1Test()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 4000 - 1, 4000 - 1, 4000 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var intensities = new ushort[] { 30, 54, 15 };
            var returnVals = intensities.Select(x => new[] { x }).ToArray();

            var master = new DummyReadRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(intensities, lili.SetIntensities1);
        }
        [Test]
        public void ReadIntensities2Test()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 4001 - 1, 4001 - 1, 4001 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var intensities = new ushort[] { 30, 54, 15 };
            var returnVals = intensities.Select(x => new[] { x }).ToArray();

            var master = new DummyReadRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(intensities, lili.SetIntensities2);
        }

        [Test]
        public void ReadBodyTemperaturesTest()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 3000 - 1, 3000 - 1, 3000 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var temperatures = new ushort[] { 30, 54, 15 };
            var returnVals = temperatures.Select(x => new[] { x }).ToArray();

            var master = new DummyReadInputRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(temperatures, lili.BodyTemperatures);
        }

        [Test]
        public void ReadTemperatures1Test()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 3011 - 1, 3011 - 1, 3011 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var temperatures = new ushort[] { 30, 54, 15 };
            var returnVals = temperatures.Select(x => new[] { x }).ToArray();

            var master = new DummyReadInputRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(temperatures, lili.Temperatures1);
        }

        [Test]
        public void ReadTemperatures2Test()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 3021 - 1, 3021 - 1, 3021 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var temperatures = new ushort[] { 30, 54, 15 };
            var returnVals = temperatures.Select(x => new[] { x }).ToArray();

            var master = new DummyReadInputRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(temperatures, lili.Temperatures2);
        }
        [Test]
        public void ReadFanEnablesTest()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 1001 - 1, 1001 - 1, 1001 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var values = new[] { false, false, true };
            var returnVals = values.Select(x => new[] { x }).ToArray();

            var master = new DummyReadCoilsModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(values, lili.FanEnables);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SetFanEnableTest(bool value)
        {
            var registers = new ushort[] { 1001 - 1 };
            var values = new[] { value };
            var master = new DummyBroadcastWriteCoilModbusMaster();
            var lili = new LinearLight(master, 0);
            lili.FanEnable = value;
            Assert.AreEqual(registers, master.CalledCoilAdresses);
            Assert.AreEqual(values, master.CalledValues);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SetSwTriggerTest(bool value)
        {
            var registers = new ushort[] { 1000 - 1 };
            var values = new[] { value };
            var master = new DummyBroadcastWriteCoilModbusMaster();
            var lili = new LinearLight(master, 0);
            lili.SwTrigger = value;
            Assert.AreEqual(registers, master.CalledCoilAdresses);
            Assert.AreEqual(values, master.CalledValues);
        }
    }
}
