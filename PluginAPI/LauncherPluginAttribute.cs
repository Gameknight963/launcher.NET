using Semver;

namespace launcherdotnet.PluginAPI
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class LauncherPluginAttribute : Attribute
    {
        public Type EntryType { get; }
        public SemVersion TargetApiVersion { get; }

        public LauncherPluginAttribute(Type entryType, SemVersion version)
        {
            EntryType = entryType;
            TargetApiVersion = version;
        }
    }
}
