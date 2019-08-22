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
            var registers = new ushort[] { 4215 - 1, 4225 - 1 };
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
            var registers = new ushort[] { 4109 - 1 };
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
            var startAddresses = new ushort[] { 4215 - 1, 4215 - 1, 4215 - 1, };
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
            var startAddresses = new ushort[] { 4225 - 1, 4225 - 1, 4225 - 1, };
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
            var startAddresses = new ushort[] { 3101 - 1, 3101 - 1, 3101 - 1, };
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
            var startAddresses = new ushort[] { 3201 - 1, 3201 - 1, 3201 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var temperatures = new ushort[] { 30, 54, 15 };
            var returnVals = temperatures.Select(x => new[] { x }).ToArray();

            var master = new DummyReadInputRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(temperatures, lili.LedTemperatures);
        }

        [Test]
        public void ReadVolts1Test()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 3213 - 1, 3213 - 1, 3213 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var voltages = new[] { 3.123, 54, 15 };
            var returnVals = voltages.Select(x => new[] { (ushort) (x * 1000) }).ToArray();

            var master = new DummyReadInputRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(voltages, lili.Volts1);
        }

        [Test]
        public void ReadVolts2Test()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 3223 - 1, 3223 - 1, 3223 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var voltages = new[] { 3.123, 54, 15 };
            var returnVals = voltages.Select(x => new[] { (ushort)(x * 1000) }).ToArray();

            var master = new DummyReadInputRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(voltages, lili.Volts2);
        }

        [Test]
        public void ReadAmps1Test()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 3214 - 1, 3214 - 1, 3214 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var currents = new[] { 3.123, 0.054, 15.123 };
            var returnVals = currents.Select(x => new[] { (ushort)(x * 1000) }).ToArray();

            var master = new DummyReadInputRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(currents, lili.Amperes1);
        }


        [Test]
        public void ReadAmps2Test()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 3224 - 1, 3224 - 1, 3224 - 1, };
            var lengths = new ushort[] { 1, 1, 1 };
            var currents = new[] { 3.123, 0.054, 15.123 };
            var returnVals = currents.Select(x => new[] { (ushort)(x * 1000) }).ToArray();

            var master = new DummyReadInputRegistersModbusMaster(addresses, startAddresses, lengths, returnVals);
            var lili = new LinearLight(master, addresses.Length);
            CollectionAssert.AreEqual(currents, lili.Amperes2);
        }

        [Test]
        public void ReadFanEnablesTest()
        {
            var addresses = new byte[] { 1, 2, 3 };
            var startAddresses = new ushort[] { 2001 - 1, 2001 - 1, 2001 - 1, };
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
            var registers = new ushort[] { 2001 - 1 };
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
            var registers = new ushort[] { 2000 - 1 };
            var values = new[] { value };
            var master = new DummyBroadcastWriteCoilModbusMaster();
            var lili = new LinearLight(master, 0);
            lili.SwTrigger = value;
            Assert.AreEqual(registers, master.CalledCoilAdresses);
            Assert.AreEqual(values, master.CalledValues);
        }
    }
}
