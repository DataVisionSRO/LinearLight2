# LinearLight2

Library to control DataVision LinearLight (LiLi) series devices.

For more info on LiLi series, see https://datavision.software/intelligent-high-power-linear-light/

## Using the library

The library consists of two nuget packages -- LinearLight2 and LinearLight2.NModbus. Both packages can be downloaded from nuget.org.

The LinearLight2 package is a core package for controling of linear light. If you wish to use NModbus library to communicate with the device,
reference also LinearLight.Nmodbus. The second library provides interfaces implementations to be able to use NModbus (https://github.com/NModbus/NModbus)
for communication with the linear light device.

## Using LinearLight2.NModbus

The NModbus library requires for the user to provide IStreamResource class. Implementation using .NET standard libraries may look for example like this:

```C#
public class Communicator : IStreamResource
{
    private readonly SerialPort serial;

    public Communicator(string comport)
    {
        serial = new SerialPort(comport, 38400, Parity.Even);
        serial.Open();
    }

    public void Dispose()
    {
        serial?.Dispose();
    }

    public void DiscardInBuffer()
    {
        serial?.DiscardInBuffer();
    }

    public int Read(byte[] buffer, int offset, int count)
    {
        return serial.Read(buffer, offset, count);
    }

    public void Write(byte[] buffer, int offset, int count)
    {
        serial.Write(buffer, offset, count);
    }

    public int InfiniteTimeout => -1;
    
    public int ReadTimeout
    {
        get => serial.ReadTimeout;
        set => serial.ReadTimeout = value;
    }

    public int WriteTimeout
    {
        get => serial.WriteTimeout;
        set => serial.WriteTimeout = value;
    }
}
```

## Using LinearLight2 library

A basic usage of library may look like this:

```C#
public class Example
{
    public static void Main()
    {
        using var streamResource = new Communicator("COM1");

        var factory =
            new LinearLightFactory(new ModbusRtuMaster(streamResource));
        // create control instance
        var lili = factory.CreateLinearLight<ILinearLightV103>();

        // It is important to do the initial setting properly. Therefore we want
        // to set up everything segment by segment. The commented out part of code below
        // would do the same using broadcast messages. Note that broadcast messages do not
        // confirm the reception of messages -- therefore should be checked later).
        // lili.Intensity = 50;
        // lili.FanMode = FanMode.Automatic;
        // lili.TriggerMode = TriggerMode.SwTrigger;
        // lili.SwTrigger = true;
        foreach (var segment in lili.Segments)
        {
            //set intensity
            segment.SetIntensity1 = 50;
            segment.SetIntensity2 = 50;
            //set fan mode to automatic
            segment.FanMode = FanMode.Automatic;
            //set SW trigger
            segment.TriggerMode = TriggerMode.SwTrigger;
            //set light on
            segment.SwTrigger = true;
        }

        while (true)
        {
            // Turn light off using broadcast message -- NOTE that broadcast message
            // can get lost on a bus, as it is not a confirmed message.
            //
            // Direct writing in lili properties will end with sending a broadcast
            // messages. If you want to have a direct communication instead, access
            // through individual segments in lili.Segments
            lili.SwTrigger = false;
            Thread.Sleep(500);
            Console.Out.WriteLine("Temperatures are: " + string.Join(", ",
                lili.Segments.Select(x => $"{x.BodyTemperature}°C")));
            Console.Out.WriteLine("Fan speeds are: " + string.Join(", ",
                lili.Segments.Select(x => $"{x.FanCurrentRpm} rpm")));
            // turn light on using broadcast message -- NOTE that broadcast message
            // can get lost on a bus, as it is not a confirmed message
            lili.SwTrigger = true;
            //check that light has really turned on
            if (lili.Segments.Any(x => !x.SwTrigger))
            {
                Console.Error.WriteLine("Some messages have been lost.");
            }
        }
    }
}
```
