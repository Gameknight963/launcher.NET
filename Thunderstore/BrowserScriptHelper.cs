namespace launcherdotnet.Thunderstore
{
    [System.Runtime.InteropServices.ComVisible(true)]
    public class BrowserScriptHelper
    {
        public void OnImageClicked(string url)
        {
            LauncherLogger.WriteLine($"Image clicked: {url}");
        }
    }
}
