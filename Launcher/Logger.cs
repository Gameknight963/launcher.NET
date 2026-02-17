using launcherdotnet.PluginAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace launcherdotnet
{
    public static class LauncherLogger
    {
        private static void WriteAndResetColor(string message)
        {
            try
            {
                Console.WriteLine(message);
            }
            finally
            {
                Console.ResetColor();
            }
        }
        public static void Write(string message, bool force = false)
        {
            if (LauncherSettings.Settings.VerboseLogging || force)
            {
                Console.Write(message);
            }
        }
        public static void WriteLine(string message, bool force = false)
        {
            if (LauncherSettings.Settings.VerboseLogging || force)
            {
                Console.WriteLine(message);
            }
        }
        public static void Highlight(string message, bool force = false)
        {
            if (LauncherSettings.Settings.VerboseLogging || force)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                WriteAndResetColor(message);
            }
        }
        public static void Success(string message, bool force = false)
        {
            if (LauncherSettings.Settings.VerboseLogging || force)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                WriteAndResetColor(message);
            }
        }
        public static void BigSuccess(string message, bool force = false)
        {
            if (LauncherSettings.Settings.VerboseLogging || force)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                WriteAndResetColor(message);
            }
        }
        public static void Error(string message, bool force = true)
        {
            if (LauncherSettings.Settings.VerboseLogging || force)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteAndResetColor(message);
            }
        }
        public static void BigError(string message, bool force = true)
        {
            if (LauncherSettings.Settings.VerboseLogging || force)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Red;
                WriteAndResetColor(message);
            }
        }
    }
}
