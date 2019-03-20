using System;
using System.Collections.Generic;
using System.Linq;
using NModbus;
using NModbus.IO;

namespace LinearLight2
{
    public class LinearLight
    {

        private readonly IModbusSerialMaster modbusSerialMaster;
        private ushort InputRegisterStartAddr = 1000 - 1;
        private ushort HoldingRegister = 2000 - 1;
        private const byte SlaveAddress = 0x0A;

        public LinearLight(IStreamResource streamResource)
        {
            var modbusFactory = new ModbusFactory();

            modbusSerialMaster = modbusFactory.CreateRtuMaster(streamResource);
            modbusSerialMaster.Transport.WriteTimeout = 1000;
            modbusSerialMaster.Transport.ReadTimeout = 1000;
            modbusSerialMaster.Transport.Retries = 3;
        }

        public List<int> Temperatures
        {
            get
            {
                var readInputRegisters =
                    modbusSerialMaster.ReadInputRegisters(SlaveAddress, InputRegisterStartAddr, 4);

                return readInputRegisters.Select(x => (int) x)
                                         .ToList();
            }
        }

        public int Intensity
        {
            get => modbusSerialMaster.ReadHoldingRegisters(SlaveAddress, HoldingRegister, 1)[0];
            set =>
                modbusSerialMaster.WriteSingleRegister(SlaveAddress, HoldingRegister, ushort.Parse(value.ToString()));
        }
    }
}