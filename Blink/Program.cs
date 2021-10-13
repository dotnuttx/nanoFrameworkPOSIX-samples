using System;
using System.Diagnostics;
using System.Threading;
using System.Device.Gpio;
using nanoFramework.Runtime.Native;
using DotnetNFPosix;

Debug.WriteLine($"Running nanoFramework on {SystemInfo.OEMString}");

try
{
    GpioController gpioController = new GpioController();
    int ledPinNumber;
    PinValue ledValue = PinValue.Low;
    GpioPin led;

    switch (SystemInfo.TargetName) {
        case Platforms.STARFIVE_JH7100:
            // pin 16 in the header is the gpio21
            ledPinNumber = 21;
        break;
        case Platforms.PI_ZERO:
            // pin 16 in the header is the gpio23
            ledPinNumber = 23;
        break;
        case Platforms.PI_PICO:
            // onboard LED
            ledPinNumber = 25;
        break;
        case Platforms.ESP32_C3:
            // pin 6 from DevKit is gpio8
            ledPinNumber = 7;
        break;
        case Platforms.NEZHA_ALLWINNER_D1:
            // pin 16 in the header is the ioexpander pp1
            ledPinNumber = 1;
        break;
        case Platforms.WSL:
            Debug.WriteLine($"The platform {SystemInfo.Platform}:{SystemInfo.TargetName} does not have GPIO support!");
            return;
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
