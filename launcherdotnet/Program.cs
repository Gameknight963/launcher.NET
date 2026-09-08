using launcherdotnet.Launcher;
using launcherdotnet.Launcher.Forms;
using launcherdotnet.Launcher.Settings;
using launcherdotnet.Styling;

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

            JsonTheme.RegisterAll();
            LauncherSettings.Load();

            LauncherLogger.WriteLine("Hello world!", true);
            LauncherLogger.WriteLine("Vesbose logging is enabled.");

            if (LauncherSettings.Settings.WaitForPlugins)
            {
                PluginManager.LoadPlugins().GetAwaiter().GetResult();
            }
            else
            {
                PluginManager.LoadPlugins().ContinueWith(t =>
                {
                    if (t.IsFaulted && t.Exception != null)
                    {
                        if (LauncherSettings.Settings.DisableExceptionHandling)
                        {
                            Exception ex = t.Exception.Flatten().InnerException ?? t.Exception;
                            Form? mainForm = Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null;
                            if (mainForm != null && mainForm.IsHandleCreated && mainForm.InvokeRequired)
                            {
                                mainForm.BeginInvoke(() => System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw());
                            }
                            else
                            {
                                ThreadPool.QueueUserWorkItem(_ => System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(ex).Throw());
                            }
                        }
                    }
                }, TaskScheduler.Default);
            }
            Application.Run(new LauncherForm());
        }
    }
}