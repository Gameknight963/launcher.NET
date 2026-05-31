using Semver;

namespace launcherdotnet.PluginAPI
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class LauncherPluginAttribute(Type entryType, string name, string description, string targetApiVersion) : Attribute
    {
        public Type EntryType { get; } = entryType;
        public string Name { get; } = name;
        public string Description { get; } = description;
        public SemVersion TargetApiVersion { get; } = SemVersion.Parse(targetApiVersion);
    }
}
