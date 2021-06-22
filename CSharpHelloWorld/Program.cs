using System.Diagnostics;
using System.Threading;
using nanoFramework.Runtime.Native;

namespace CSharpHelloWorld
{
    public class Program
    {
        public static void Main()
        {
            Debug.WriteLine($"Hello from nanoFramework on {SystemInfo.OEMString}!");

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
