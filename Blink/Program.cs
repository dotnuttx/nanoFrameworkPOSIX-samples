using System;
using System.Diagnostics;
using System.Threading;
using System.Device.Gpio;
using nanoFramework.Runtime.Native;

Debug.WriteLine($"Running nanoFramework on {SystemInfo.OEMString}");

try
{
    GpioController gpioController = new GpioController();
    int ledPinNumber;
    PinValue ledValue = PinValue.Low;
    GpioPin led;

    switch (SystemInfo.TargetName) {
        case "beagle-v":
            // pin 16 in the header is the gpio21
            ledPinNumber = 21;
        break;
        case "pi-zero":
            // pin 16 in the header is the gpio23
            ledPinNumber = 23;
        break;
        case "pi-pico":
            // onboard LED
            ledPinNumber = 25;
        break;
        default:
            throw new Exception($"Your target [{SystemInfo.TargetName}] does not support GPIOs");
    }

    // initiliaze pin
    led = gpioController.OpenPin(ledPinNumber, PinMode.Output);

    // blink forever
    while (true)
    {
        Debug.WriteLine($"Blinking {ledValue}");
        
        ledValue = !(bool)ledValue;
        led.Write(ledValue);

        Thread.Sleep(500);
    }
}
catch (Exception ex)
{
    Debug.WriteLine(ex.Message);
    Debug.WriteLine(ex.StackTrace);
}
