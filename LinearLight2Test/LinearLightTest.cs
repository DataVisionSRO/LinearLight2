using System;
using System.Linq;
using LinearLight2;
using NUnit.Framework;

namespace LinearLight2Test
{
    [TestFixture]
    [Explicit("Tests require connection to light and manual usage")]
    public class LinearLightTest
    {
        private string comport = "COM28";

        [Test]
        public void ReadTemperatureRegisters()
        {
            var resource = new Communicator(comport);
            var lili = new LinearLight(resource,3);
            foreach (var liliTemperature in lili.Temperatures)
            {
                Console.Out.WriteLine("temperature is " + liliTemperature);
            }
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        public void WriteAndReadHoldingRegister(int number)
        {
            using (var streamResource = new Communicator(comport))
            {   
                   
                var lili = new LinearLight(streamResource,3);
                 
                lili.Intensity = number;
                CollectionAssert.AreEqual(Enumerable.Repeat(number,3), lili.Intensities);
            }
        }
    }
}
