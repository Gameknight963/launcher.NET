using System;
using System.Runtime.InteropServices;

class ConsoleHelper
{
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    private static extern bool FreeConsole();

    public static void Show()
    {
        AllocConsole();
    }

    public static void Hide()
    {
        FreeConsole();
    }
}
