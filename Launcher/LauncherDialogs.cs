using launcherdotnet.Launcher.Forms;

namespace launcherdotnet.Launcher
{
    public static class LauncherDialogs
    {
        public static string? QueryLabel(string defaultText, string prompt = "Enter a label for this instance:", string title = "Set Label")
        {
            string? result = CoolInputBox.Prompt(prompt, title, defaultText);
            if (result is null) return null;
            if (result != result.Trim())
            {
                CoolMessageBox.Show(
                    "Label must not contain trailing or leading whitespace.",
                    "Invalid name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return null;
            }
            return result;
        }
    }
}
