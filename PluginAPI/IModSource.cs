using launcherdotnet.Launcher;

namespace launcherdotnet.PluginAPI
{
    public interface IModSource : ILauncherPlugin
    {
        /// <summary>
        /// User friendly display name
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Unique identifier for this mod source, used to match against <see cref="GameInfo"/>.
        /// </summary>
        /// 
        /// <remarks>
        /// Follow the convention 'namespace.pluginname' or 'owner.pluginname'.
        /// </remarks>
        string Id { get; }

        /// <summary>
        /// Opens the mod browser for this game.
        /// </summary>
        /// 
        /// <param name="game">
        /// The game the user has requested to open the mod browser for.
        /// </param>
        Task OpenModBrowser(GameInfo game);

        /// <summary>
        /// Returns installed mods for display in the package manager.
        /// </summary>
        IEnumerable<InstalledMod> GetInstalledMods(GameInfo game);

        /// <summary>
        /// Called when the user has selected to uninstall a package through the package manager.
        /// </summary>
        /// <param name="game">The game that a package is being uninstalled from</param>
        /// <param name="mods">The mods that are being uninstalled</param>
        /// <returns><see langword="false"/> if the uninstallation should be cancelled, otherwise <see langword="true"/>.</returns>
        bool UninstallMods(GameInfo game, List<InstalledMod> mods);
    }
}
