using launcherdotnet.Launcher.Forms.Thunderstore;
using launcherdotnet.Networking;
using launcherdotnet.Styling;
using launcherdotnet.Thunderstore;
using Markdig;
using Markdown.ColorCode;
using Semver;
using Svg;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace launcherdotnet.Launcher.Forms
{
    public partial class ThunderstoreModBrowser : ThemeableForm
    {
        private List<ThunderstorePackageSlim> _packages = [];
        private List<string> _chunkUrls = [];
        private int _currentChunk = 0;
        private bool _isLoading = false;
        private readonly string _slug;
        private readonly Dictionary<int, List<ThunderstoreVersion>> _versionCache = [];
        private readonly Dictionary<int, string> _readmeCache = [];
        private string? _currentReadme;
        private readonly GameInfo _game;

        private readonly HashSet<ThunderstorePackageInstallContext> _selectedForInstall = [];

        private static readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UseColorCode()
            .Build();

        public ThunderstoreModBrowser(GameInfo game)
        {
            InitializeComponent();
            _game = game;
            okButton.Enabled = false;
            CancelButton = cancelButton;
            AcceptButton = okButton;
            StartPosition = FormStartPosition.CenterParent;
            modsLv.VirtualMode = true;
            modsLv.RetrieveVirtualItem += ModsLv_RetrieveVirtualItem;
            UpdateModsLv(game);
            _slug = game.ThunderstoreCommunitySlug ??
                throw new InvalidOperationException("Game has no thunderstore slug");
            FormClosed += (s, e) =>
            {
                modsLv.VirtualListSize = 0;
                _packages = [];
                GC.Collect();
                GC.WaitForPendingFinalizers();
            };

            descriptionPanel.ImageLoad += async (sender, args) =>
            {
                if (Path.GetExtension(args.Src)?.Equals(".svg", StringComparison.OrdinalIgnoreCase) == true
                    || args.Src.Contains("shields.io"))
                {
                    args.Handled = true;
                    byte[] data = await LauncherHttp.Client.GetByteArrayAsync(args.Src);
                    using MemoryStream ms = new(data);
                    SvgDocument svgDoc = SvgDocument.Open<SvgDocument>(ms);
                    Bitmap svgImg = new((int)svgDoc.Width, (int)svgDoc.Height, PixelFormat.Format32bppArgb);
                    svgDoc.Draw(svgImg);
                    args.Callback(svgImg);
                }
            };
            descriptionPanel.LinkClicked += (sender, e) =>
            {
                if (e.Link.StartsWith("img:"))
                {
                    e.Handled = true;
                    string url = e.Link[4..];
                    BigImageViewer.Show(url);
                }
            };
        }

        private void ModsLv_RetrieveVirtualItem(object? sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = new ListViewItem(_packages[e.ItemIndex].Name);
            if (!_isLoading && _currentChunk < _chunkUrls.Count && e.ItemIndex == _packages.Count - 1)
            {
                int visibleCount = modsLv.ClientSize.Height / (modsLv.GetItemRect(0).Height);
                int topIndex = modsLv.TopItem?.Index ?? 0;
                if (topIndex + visibleCount >= _packages.Count - 10)
                    _ = LoadNextChunk();
            }
        }

        private async Task LoadNextChunk()
        {
            if (_currentChunk >= _chunkUrls.Count) return;
            _isLoading = true;
            int topIndex = modsLv.TopItem?.Index ?? 0;
            LauncherLogger.WriteLine($"Fetching chunk {_currentChunk}");
            List<ThunderstorePackageSlim> packages = await ThunderstoreClient.GetPackageListChunkAsync(_chunkUrls[_currentChunk]);
            _currentChunk++;
            _packages.AddRange(packages);
            modsLv.VirtualListSize = _packages.Count;
            if (topIndex < _packages.Count)
                modsLv.TopItem = modsLv.Items[topIndex];
            _isLoading = false;
        }

        async void UpdateModsLv(GameInfo game)
        {
            if (game.ThunderstoreCommunitySlug is not string slug) return;
            downloadPnl.Visible = false;
            _packages = [];
            _currentChunk = 0;
            UseWaitCursor = true;
            _chunkUrls = await ThunderstoreClient.GetPackageListIndexAsync(slug);
            if (_chunkUrls.Count > 0)
                await LoadNextChunk();
            UseWaitCursor = false;
            downloadPnl.Visible = true;
            modsLv.Items[0].Selected = true;
            LauncherLogger.WriteLine("Done with initial modlist fetch");
        }

        private async void modsLv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modsLv.SelectedIndices.Count == 0)
            {
                downloadPnl.Visible = false;
                versionsCb.Items.Clear();
                descriptionPanel.Text = "";
                return;
            }
            descriptionPanel.Text =
                $"<html><body style='background:{ColorTranslator.ToHtml(BackColor)};color:white;font-family:Segoe UI'>Loading...</body></html>";

            UseWaitCursor = true;
            int index = modsLv.SelectedIndices[0];
            ThunderstorePackageSlim slim = _packages[index];

            ThunderstorePackage? full = null;

            if (!_versionCache.TryGetValue(index, out List<ThunderstoreVersion>? versions))
            {
                full = await slim.FetchFullPackageAsync();
                if (full is null)
                {
                    LauncherLogger.Warn("Package null, skipping...");
                    UseWaitCursor = false;
                    return;
                }
                versions = await full.FetchVersionsAsync(_slug);
                _versionCache[index] = versions;
            }

            versionsCb.Items.Clear();
            foreach (ThunderstoreVersion v in versions)
                versionsCb.Items.Add(v.VersionNumber);
            if (versionsCb.Items.Count > 0) versionsCb.SelectedIndex = 0;

            if (!_readmeCache.TryGetValue(index, out string? readmeContent))
            {
                full ??= await slim.FetchFullPackageAsync();
                if (full != null)
                {
                    readmeContent = await ThunderstoreClient.GetPackageReadmeAsync(full);
                    _readmeCache[index] = readmeContent;
                }
            }

            if (readmeContent == null)
            {
                descriptionPanel.Text = "<p>Readme not found.</p>";
                UseWaitCursor = false;
                return;
            }
            UpdateReadme(readmeContent);
            downloadPnl.Visible = true;
            SetDownloadPanelBasedOnContext();
            UseWaitCursor = false;
            LauncherLogger.WriteLine($"Done fetching info for {slim.Name}");
        }

        private void UpdateReadme(string readmeContent)
        {
            _currentReadme = readmeContent;
            string fg = ColorTranslator.ToHtml(ForeColor);
            string raisedBg = ColorTranslator.ToHtml(ControlPaint.Light(BackColor, 0.1f));
            string body = Markdig.Markdown.ToHtml(_currentReadme, _pipeline);
            body = Regex.Replace(body, @"\n</code>", "</code>");
            string html = $@"<html><head>
                <style>
                    body {{ font-family: Segoe UI, sans-serif; font-size: 13px; color: {fg}; margin: 8px; }}
                    img {{ max-width: 280px !important; height: auto !important; cursor: pointer; }}
                    table {{ border-collapse: collapse; margin: 8px 0; }}
                    th, td {{ border: 1px solid #aaa; padding: 4px 10px; }}
                    th {{ font-weight: bold; background-color: {raisedBg}; }}
                    code {{ font-family: Consolas, monospace; font-size: 12px; background-color: {raisedBg}; padding: 1px 4px; }}
                    pre {{ background-color: {raisedBg}; padding: 10px 12px; overflow-x: auto; }}
                    pre code {{ background-color: transparent; padding: 0; }}
                    * {{ margin-top: 0; }}
                    p:last-child, pre:last-child {{ margin-bottom: 0; }}
                </style>
                </head><body>{body}</body></html>";
            html = Regex.Replace(html, """<img\s+src="([^"]+)"([^>]*)>""", """<a href="img:$1"><img src="$1"$2></a>""");
            descriptionPanel.Text = html;
        }

        protected override void OnThemeWasApplied()
        {
            if (_currentReadme != null) UpdateReadme(_currentReadme);
        }

        private ThunderstorePackageInstallContext? GetSelectedInstallContext()
        {
            if (versionsCb.SelectedItem == null || modsLv.SelectedIndices.Count == 0) return null;
            int index = modsLv.SelectedIndices[0];
            ThunderstorePackageSlim slim = _packages[index];
            string selectedVersion = versionsCb.SelectedItem.ToString()!;
            if (!_versionCache.TryGetValue(index, out List<ThunderstoreVersion>? versions)) return null;
            ThunderstoreVersion? version = versions.FirstOrDefault(v => v.VersionNumber == selectedVersion);
            if (version == null) return null;
            return new ThunderstorePackageInstallContext(slim.Name, version.DownloadUrl, selectedVersion, slim.Owner);
        }

        private void SetDownloadPanelBasedOnContext()
        {
            if (versionsCb.SelectedItem == null || modsLv.SelectedIndices.Count == 0) return;
            ThunderstorePackageInstallContext? context = GetSelectedInstallContext();
            if (context == null) return;
            bool contains = _selectedForInstall.Contains(context);
            if (!contains)
                _selectedForInstall.Remove(context);
            downloadBtn.Text = contains ? "Deselect mod for download" : "Select mod for download";
        }

        private void AddSelectedContext()
        {
            ThunderstorePackageInstallContext? context = GetSelectedInstallContext();
            if (context == null) return;
            bool added = _selectedForInstall.Add(context);
            if (!added) _selectedForInstall.Remove(context);
            downloadBtn.Text = added ? "Deselect mod for download" : "Select mod for download";
            okButton.Enabled = _selectedForInstall.Count > 0;
        }

        private void downloadBtn_Click(object sender, EventArgs e)
        {
            AddSelectedContext();
        }

        private async void okButton_Click(object sender, EventArgs e)
        {
            UseWaitCursor = true;
            okButton.Enabled = false;

            List<ThunderstoreVersion> pkgs = [];
            // name -> (version object, display label)
            Dictionary<string, (ThunderstoreVersion Version, string Label)> depsMap = [];

            // fetch version objects and resolve dependencies for each selected package
            foreach (ThunderstorePackageInstallContext p in _selectedForInstall)
            {
                LauncherLogger.WriteLine($"Fetching version object for {p.Name}");
                ThunderstoreVersion? v = await p.FetchThunderstoreVersionAsync();
                if (v is null)
                {
                    LauncherLogger.Error($"Unable to fetch version object for {p.Name}. Cancelling");
                    UseWaitCursor = false;
                    okButton.Enabled = true;
                    return;
                }
                pkgs.Add(v);

                foreach (ThunderstoreVersion dep in await v.FetchDependenciesAsync())
                {
                    if (depsMap.TryGetValue(dep.Name, out (ThunderstoreVersion Version, string Label) existing))
                    {
                        // same version required by multiple packages, no conflict
                        if (existing.Version.VersionNumber == dep.VersionNumber)
                            continue;

                        // different versions required, so pick the latest and warn the user
                        SemVersion existingVer = SemVersion.Parse(existing.Version.VersionNumber, SemVersionStyles.Any);
                        SemVersion newVer = SemVersion.Parse(dep.VersionNumber, SemVersionStyles.Any);
                        int cmp = SemVersion.ComparePrecedence(newVer, existingVer);
                        ThunderstoreVersion winner = cmp > 0 ? dep : existing.Version;
                        depsMap[dep.Name] = (winner, $" {winner.VersionNumber} (dependency of multiple packages)");
                        CoolMessageBox.Show(
                            $"Dependency version conflict for {dep.Name}:\n" +
                            $"Required {existing.Version.VersionNumber} and {dep.VersionNumber}\n" +
                            $"Automatically selecting {winner.VersionNumber}.",
                            "Dependency conflict",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        depsMap[dep.Name] = (dep, $" {dep.VersionNumber} (dependency of {p.Name})");
                    }
                }
            }

            // remove deps that the user explicitly selected, but warn if versions differ
            HashSet<string> selectedNames = pkgs.Select(p => p.Name).ToHashSet();
            foreach (string name in selectedNames)
            {
                if (depsMap.TryGetValue(name, out (ThunderstoreVersion Version, string Label) existing))
                {
                    ThunderstoreVersion selectedPkg = pkgs.First(p => p.Name == name);
                    if (existing.Version.VersionNumber != selectedPkg.VersionNumber)
                    {
                        CoolMessageBox.Show(
                            $"Dependency version conflict for {name}:\n" +
                            $"Required {existing.Version.VersionNumber} but you selected {selectedPkg.VersionNumber}.\n" +
                            $"Using your selected version.",
                            "Dependency conflict",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Asterisk);
                    }
                    depsMap.Remove(name);
                }
            }

            List<ThunderstoreVersion> deps = depsMap.Values.Select(x => x.Version).ToList();

            UseWaitCursor = false;
            okButton.Enabled = true;

            List<string> pkgStrings = pkgs.Select(p => $"{p.Name} v{p.VersionNumber}").ToList();
            List<string> depStrings = depsMap.Values.Select(x => $"{x.Version.Name} v{x.Version.VersionNumber}{x.Label}").ToList();

            if (new ReviewAndConfirm(pkgStrings.Concat(depStrings), deps.Count).ShowDialog() != DialogResult.OK)
                return;

            new ThunderstoreModInstaller(_game, pkgs, deps).ShowDialog();
            Close();
        }
    }
}