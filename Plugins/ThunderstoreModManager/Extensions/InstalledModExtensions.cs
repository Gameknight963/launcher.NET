using launcherdotnet.Launcher;

namespace ThunderstoreModManager.Extensions
{
    public static class InstalledModExtensions
    {
        public static string DependencyString(this InstalledMod m)
        {
            return $"{m.Owner}-{m.Name}-{m.Version}";
        }
        public static bool DependencyStringEquals(this InstalledMod m, InstalledMod other)
        {
            return m.DependencyString() == other.DependencyString();
        }
    }
}
