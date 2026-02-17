using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace launcherdotnet
{

    internal static class StartupHelper
    {
        private const string KeyName = "launcherdotnet";

        public static void EnableRunOnStartup()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true)!;
            key.SetValue(KeyName, $"\"{Application.ExecutablePath}\"");
        }

        public static void DisableRunOnStartup()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true)!;
            key.DeleteValue(KeyName, false);
        }

        public static bool IsEnabled()
        {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
            return key?.GetValue(KeyName) != null;
        }
    }
}

