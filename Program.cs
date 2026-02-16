namespace launcherdotnet
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            LauncherSettings.Load();
            if (LauncherSettings.Settings.OpenDebugConsole)
            {
                ConsoleHelper.Show();
            }
            LauncherLogger.WriteLine("Hello world!", true);
            LauncherLogger.WriteLine("Vesbose logging is enabled.");
            Application.Run(new LauncherForm());
        }
    }
}