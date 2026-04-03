using System;
using System.Runtime.InteropServices;

internal class ConsoleHelper
{
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    private static extern bool FreeConsole();

    public static bool ConsoleShown = false;

    public static void Show()
    {
        ConsoleShown = true;
        AllocConsole();
        Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        Console.SetError(new StreamWriter(Console.OpenStandardError()) { AutoFlush = true });
        Console.SetIn(new StreamReader(Console.OpenStandardInput()));
    }

    public static void Hide()
    {
        ConsoleShown = false;
        FreeConsole();
        Console.SetOut(TextWriter.Null);
        Console.SetError(TextWriter.Null);
        ConsoleShown = false;
    }
}
