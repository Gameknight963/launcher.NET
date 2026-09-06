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
    private static StreamWriter? _outWriter;
    private static StreamWriter? _errWriter;
    private static StreamReader? _inReader;

    public static void Show()
    {
        ConsoleShown = true;
        AllocConsole();
        _outWriter = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
        _errWriter = new StreamWriter(Console.OpenStandardError()) { AutoFlush = true };
        _inReader = new StreamReader(Console.OpenStandardInput());
        Console.SetOut(_outWriter);
        Console.SetError(_errWriter);
        Console.SetIn(_inReader);
    }

    public static void Hide()
    {
        ConsoleShown = false;
        Console.SetOut(TextWriter.Null);
        Console.SetError(TextWriter.Null);
        _outWriter?.Dispose();
        _errWriter?.Dispose();
        _inReader?.Dispose();
        _outWriter = null;
        _errWriter = null;
        _inReader = null;
        FreeConsole();
    }
}
