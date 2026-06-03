namespace launcherdotnet.PluginAPI
{
    public abstract class GameInstallerBase : IGameInstaller
    {
        /// <inheritdoc />
        public virtual Task Initialize() => Task.CompletedTask;

        /// <inheritdoc />
        public virtual IEnumerable<string>? GetReleases() => null;

        /// <inheritdoc />
        public virtual LabelQueryTime PromptForLabel =>
            LabelQueryTime.Never;

        /// <inheritdoc/>
        public abstract string GameName { get; }

        /// <inheritdoc />
        public abstract Task<PluginGameInfo?> Install(
            string installDir,
            IProgress<double> progress,
            IProgress<string> status,
            string? version = null);
    }
}
