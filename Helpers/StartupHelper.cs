using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace launcherdotnet.Helpers
{

    internal static class StartupHelper
    {
        private const string _keyName = "launcherdotnet";

        public static void EnableRunOnStartup()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true)!;
            key.SetValue(_keyName, $"\"{Application.ExecutablePath}\"");
        }

        public static void DisableRunOnStartup()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true)!;
            key.DeleteValue(_keyName, false);
        }

        public static bool IsEnabled()
        {
            using RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
            return key?.GetValue(_keyName) != null;
        }
    }
}