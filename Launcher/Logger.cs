using launcherdotnet.PluginAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace launcherdotnet
{
    public static class LauncherLogger
    {
        // assign it to a variable in case we ever want to add a new condition
        private static bool CanWrite => LauncherSettings.Settings.VerboseLogging;

        private static void WriteAndResetColor(string message)
        {
            try
            {
                Console.WriteLine(message);
                Console.ResetColor();
            }
            catch (UnauthorizedAccessException) { return; }
        }
        public static void Write(string message, bool force = false)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            if (!(CanWrite || force)) return;
            Console.Write(message);
        }
        public static void WriteLine(string message, bool force = false)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            if (!(CanWrite || force)) return;
            Console.WriteLine(message);
        }
        public static void Highlight(string message, bool force = false)
        {
            if (!(CanWrite || force)) return;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            WriteAndResetColor(message);
        }
        public static void Warn(string message, bool force = false)
        {
            if (!(CanWrite || force)) return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteAndResetColor(message);
        }
        public static void Success(string message, bool force = false)
        {
            if (!(CanWrite || force)) return;
            Console.ForegroundColor = ConsoleColor.Green;
            WriteAndResetColor(message);
        }
        public static void BigSuccess(string message, bool force = false)
        {
            if (!(CanWrite || force)) return;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            WriteAndResetColor(message);
        }
        public static void Error(string message, bool force = true)
        {
            if (!(CanWrite || force)) return;
            Console.ForegroundColor = ConsoleColor.Red;
            WriteAndResetColor(message);
        }
        public static void BigError(string message, bool force = true)
        {
            if (!(CanWrite || force)) return;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            WriteAndResetColor(message);
        }
    }
}
