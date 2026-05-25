using Semver;

namespace launcherdotnet.Thunderstore
{
    public static class ModResolver
    {
        public record ResolveResult(
            List<ThunderstoreVersion> Packages,
            List<ThunderstoreVersion> Dependencies);

        public static async Task<ResolveResult?> ResolveAsync(
            IEnumerable<ThunderstoreVersion> selected,
            Action<string>? onConflict = null)
        {
            List<ThunderstoreVersion> pkgs = [.. selected];
            // name -> (version object, display label)
            Dictionary<string, (ThunderstoreVersion Version, string Label)> depsMap = [];

            // fetch and resolve dependencies for each selected package
            foreach (ThunderstoreVersion v in pkgs)
            {
                LauncherLogger.WriteLine($"Fetching dependencies for {v.Name}");
                foreach (ThunderstoreVersion dep in await v.FetchDependenciesAsync())
                {
                    if (depsMap.TryGetValue(dep.Name, out (ThunderstoreVersion Version, string Label) existing))
                    {
                        // same version required by multiple packages, no conflict
                        if (existing.Version.VersionNumber == dep.VersionNumber)
                            continue;

                        // different versions required, pick the latest and notify caller
                        SemVersion existingVer = SemVersion.Parse(existing.Version.VersionNumber, SemVersionStyles.Any);
                        SemVersion newVer = SemVersion.Parse(dep.VersionNumber, SemVersionStyles.Any);
                        int cmp = SemVersion.ComparePrecedence(newVer, existingVer);
                        ThunderstoreVersion winner = cmp > 0 ? dep : existing.Version;
                        depsMap[dep.Name] = (winner, $" {winner.VersionNumber} (dependency of multiple packages)");
                        onConflict?.Invoke(
                            $"Dependency version conflict for {dep.Name}:\n" +
                            $"Required {existing.Version.VersionNumber} and {dep.VersionNumber}\n" +
                            $"Automatically selecting {winner.VersionNumber}.");
                    }
                    else
                    {
                        depsMap[dep.Name] = (dep, $" {dep.VersionNumber} (dependency of {v.Name})");
                    }
                }
            }

            // remove deps that the user explicitly selected, but notify if versions differ
            HashSet<string> selectedNames = pkgs.Select(p => p.Name).ToHashSet();
            foreach (string name in selectedNames)
            {
                if (depsMap.TryGetValue(name, out (ThunderstoreVersion Version, string Label) existing))
                {
                    ThunderstoreVersion selectedPkg = pkgs.First(p => p.Name == name);
                    if (existing.Version.VersionNumber != selectedPkg.VersionNumber)
                    {
                        onConflict?.Invoke(
                            $"Dependency version conflict for {name}:\n" +
                            $"Required {existing.Version.VersionNumber} but you selected {selectedPkg.VersionNumber}.\n" +
                            $"Using your selected version.");
                    }
                    depsMap.Remove(name);
                }
            }

            List<ThunderstoreVersion> deps = depsMap.Values.Select(x => x.Version).ToList();
            return new ResolveResult(pkgs, deps);
        }

        public static (List<string> PackageStrings, List<string> DependencyStrings) BuildDisplayStrings(ResolveResult result)
        {
            List<string> pkgStrings = result.Packages
                .Select(p => $"{p.Name} v{p.VersionNumber}")
                .ToList();
            List<string> depStrings = result.Dependencies
                .Select(d => $"{d.Name} v{d.VersionNumber} (dependency)")
                .ToList();
            return (pkgStrings, depStrings);
        }
    }
}
