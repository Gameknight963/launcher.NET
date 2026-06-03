namespace launcherdotnet.PluginAPI
{
    /// <summary>
    /// Base implementation of <see cref="IGameInstaller"/> providing
    /// sensible defaults for optional members
    /// </summary>
    /// <remarks>
    /// Plugin authors are encouraged to inherit from this class unless
    /// they require a different inheritance hierarchy.
    /// </remarks>
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
