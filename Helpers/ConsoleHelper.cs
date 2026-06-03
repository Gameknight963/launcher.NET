using System.Runtime.InteropServices;

internal partial class ConsoleHelper
{
    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool AllocConsole();

    [LibraryImport("kernel32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool FreeConsole();

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
