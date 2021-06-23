using System.Diagnostics;
using System.Threading;
using System.Device.Gpio;
using nanoFramework.Runtime.Native;

GpioController gpioController = new GpioController();
// GP25 (onboard LED)
GpioPin onBoardLED = gpioController.OpenPin(25, PinMode.Output);
// GP6 (pin 9)
GpioPin button = gpioController.OpenPin(6, PinMode.Input);

while (true)
{
    // blink
    onBoardLED.Toggle();

    // check if button is pressed
    if (button.Read() == PinValue.High)
    {
        Debug.WriteLine($"Running nanoFramework on {SystemInfo.TargetName}");
        Debug.WriteLine($"Platform: {SystemInfo.Platform}");
        Debug.WriteLine($"Firmware info: {SystemInfo.OEMString}");
    }

    Thread.Sleep(500);
}
