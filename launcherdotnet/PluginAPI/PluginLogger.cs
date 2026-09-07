using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;

namespace launcherdotnet.PluginAPI
{
    public static class PluginLogger
    {
        [Obsolete("Use WriteLine instead.")]
        public static void Log(string msg, bool force = false) => LauncherLogger.WriteLine(msg.ToString(), force);

        public static void WriteColor(
        object message,
        bool force = false,
        ConsoleColor textColor = ConsoleColor.White,
        ConsoleColor bgColor = ConsoleColor.Black)
        {
            Assembly? assembly = new StackTrace().GetFrame(2)?.GetMethod()?.DeclaringType?.Assembly;
            LauncherPluginAttribute? attribute = assembly?.GetCustomAttribute<LauncherPluginAttribute>();
            if (attribute == null)
            {
                LauncherLogger.WriteColor(message.ToString() ?? "", force, textColor, bgColor);
                return;
            }
            ConsoleColor color = attribute.EntryType switch
            {
                _ when typeof(ILauncherPlugin).IsAssignableFrom(attribute.EntryType) => ConsoleColor.Cyan,
                _ when typeof(IGameInstaller).IsAssignableFrom(attribute.EntryType) => ConsoleColor.DarkCyan,
                _ when typeof(IModSource).IsAssignableFrom(attribute.EntryType) => ConsoleColor.DarkMagenta,
                _ => ConsoleColor.White
            };
            LauncherLogger.WriteColor("[", force);
            LauncherLogger.WriteColor(attribute.Name, force, color);
            LauncherLogger.WriteColor("] ");
            LauncherLogger.WriteColor($"{message}", force, textColor, bgColor);
        }

        /// <summary>
        /// Writes a line in any color to the console.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="force"> <see langword="true"/> to show this message with verbose logging off;
        /// otherwise, <see langword="false"></see>.</param>
        /// <param name="textColor">The foreground <see cref="ConsoleColor"/> of this message.</param>
        /// <param name="bgColor">The background <see cref="ConsoleColor"/> of this message.</param>
        public static void WriteColorLine(object msg, bool force = false, 
            ConsoleColor textColor = ConsoleColor.White,
            ConsoleColor bgColor = ConsoleColor.Black)

            => WriteColor($"{msg}\n", force, textColor, bgColor);

        /// <summary>
        /// Writes a line to the console.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="force"> <see langword="true"/> to show this message with verbose logging off;
        /// otherwise, <see langword="false"></see>.</param>
        public static void WriteLine(object msg, bool force = false)
            => WriteColor($"{msg}\n", force, ConsoleColor.Gray, ConsoleColor.Black);

        /// <summary>
        /// Writes text to the console.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="force"> <see langword="true"/> to show this message with verbose logging off;
        /// otherwise, <see langword="false"></see>.</param>
        public static void Write(object msg, bool force = false)
            => WriteColor(msg, force, ConsoleColor.Gray, ConsoleColor.Black);

        /// <summary>
        /// Writes a white background highlighted message to the console.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="force"> <see langword="true"/> to show this message with verbose logging off;
        /// otherwise, <see langword="false"></see>.</param>
        public static void Highlight(object msg, bool force = false)
            => WriteColor($"{msg}\n", force, ConsoleColor.Black, ConsoleColor.White);

        /// <summary>
        /// Writes a yellow warning to the console.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="force"> <see langword="true"/> to show this message with verbose logging off;
        /// otherwise, <see langword="false"></see>.</param>
        public static void Warn(object msg, bool force = false)
            => WriteColor($"{msg}\n", force, ConsoleColor.Yellow, ConsoleColor.Black);

        /// <summary>
        /// Writes a happy success to the console in green.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="force"> <see langword="true"/> to show this message with verbose logging off;
        /// otherwise, <see langword="false"></see>.</param>
        public static void Success(object msg, bool force = false)
            => WriteColor($"{msg}\n", force, ConsoleColor.Green, ConsoleColor.Black);

        /// <summary>
        /// Writes a super duper happy success to the console
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="force"> <see langword="true"/> to show this message with verbose logging off;
        /// otherwise, <see langword="false"></see>.</param>
        public static void BigSuccess(object msg, bool force = false)
            => WriteColor($"{msg}\n", force, ConsoleColor.White, ConsoleColor.Green);

        /// <summary>
        /// Writes an error line to the console. The error will appear red.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="force"> <see langword="true"/> to show this message with verbose logging off;
        /// otherwise, <see langword="false"></see>.</param>
        public static void Error(object msg, bool force = true)
            => WriteColor($"{msg}\n", force, ConsoleColor.Red, ConsoleColor.Black);

        /// <summary>
        /// Writes an EVIL error to the console.
        /// </summary>
        /// <param name="msg">The message to write.</param>
        /// <param name="force"> <see langword="true"/> to show this message with verbose logging off;
        /// otherwise, <see langword="false"></see>.</param>
        public static void BigError(object msg, bool force = true)
            => WriteColor($"{msg}\n", force, ConsoleColor.White, ConsoleColor.Red);
    }
}
