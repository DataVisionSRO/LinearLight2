using System;
using System.Linq;
using System.Threading;
using LinearLight2;
using LinearLight2.NModbus;
using NUnit.Framework;

namespace LinearLight2Test.LinearLight2
{
    [TestFixture]
    public class LinearLightTester
    {
        [Test]
        [Explicit ("for manual testing only")]
        public void SetIntensity()
        {
            using (var comm = new Communicator("COM29"))
            {
                var master = new ModbusRtuMaster(comm);
                var lili = new LinearLight(master, 1);
                lili.Intensity = 10;
                //lili.FanSpeed = 100;
                lili.FanEnable = true;
                lili.SwTrigger = true;
                Thread.Sleep(500);
                Console.Out.WriteLine(string.Join(", ",lili.SetIntensities1.Select(x=>x.ToString())));
            }
        }
    }
}
