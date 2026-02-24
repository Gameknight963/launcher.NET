using launcherdotnet.PluginAPI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace launcherdotnet
{
    public static class LauncherLogger
    {
        // assign it to a variable in case we ever want to add a new condition
        private static bool CanWrite => LauncherSettings.Settings.VerboseLogging;

        public static void WriteColor(string message, 
            bool force = false, 
            ConsoleColor textColor = ConsoleColor.White, 
            ConsoleColor bgColor = ConsoleColor.Black)
        {
            if (!(CanWrite || force)) return;
            try
            {
                Console.BackgroundColor = bgColor;
                Console.ForegroundColor = textColor;
                Console.Write(message);
                Console.ResetColor();
            }
            catch (UnauthorizedAccessException) { return; }
            catch { Console.ResetColor(); }
        }
        public static void WriteColorLine(string message,
            bool force = false,
            ConsoleColor textColor = ConsoleColor.White,
            ConsoleColor bgColor = ConsoleColor.Black)
        {
            WriteColor($"{message}\n", force, textColor, bgColor);
        }

        public static void WriteLine(string message, bool force = false) { WriteColor($"{message}\n", force, ConsoleColor.Gray, ConsoleColor.Black); }
        public static void Write(string message, bool force = false) { WriteColor(message, force, ConsoleColor.Gray, ConsoleColor.Black); }

        public static void Highlight(string message, bool force = false) { WriteColor($"{message}\n", force, ConsoleColor.Black, ConsoleColor.White); }
        public static void Warn(string message, bool force = false) { WriteColor($"{message}\n", force, ConsoleColor.Yellow); }
        public static void Success(string message, bool force = false) { WriteColor($"{message}\n", force, ConsoleColor.Green); }
        public static void BigSuccess(string message, bool force = false) { WriteColor($"{message}\n", force, ConsoleColor.White, ConsoleColor.Green); }
        public static void Error(string message, bool force = true) { WriteColor($"{message}\n", force, ConsoleColor.Red); }
        public static void BigError(string message, bool force = true) { WriteColor($"{message}\n", force, ConsoleColor.White, ConsoleColor.Red); }
    }
}
