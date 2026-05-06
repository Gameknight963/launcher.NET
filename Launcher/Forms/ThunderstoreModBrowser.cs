using launcherdotnet.Styling;
using launcherdotnet.Thunderstore;
using Markdig;
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

        public ThunderstoreModBrowser(GameInfo game)
        {
            InitializeComponent();
            CancelButton = cancelButton;
            AcceptButton = okButton;
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

            descriptionBrowser.Navigating += (s, e) =>
            {
                if (e.Url is null || e.Url.ToString() == "about:blank") return;
                e.Cancel = true;
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = e.Url.ToString(),
                    UseShellExecute = true
                });
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
            _packages = [];
            _currentChunk = 0;
            UseWaitCursor = true;
            _chunkUrls = await ThunderstoreClient.GetPackageListIndexAsync(slug);
            if (_chunkUrls.Count > 0)
                await LoadNextChunk();
            UseWaitCursor = false;
        }

        private async void modsLv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modsLv.SelectedIndices.Count == 0)
            {
                versionsCb.Items.Clear();
                descriptionBrowser.DocumentText = "";
                return;
            }

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
                descriptionBrowser.DocumentText = "<p>Readme not found.</p>";
                UseWaitCursor = false;
                return;
            }
            string html = $@"<html><head>
                <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                <style>
                body {{ font-family: Segoe UI, sans-serif; font-size: 13px; }}
                img {{ max-width: 280px !important; height: auto !important; cursor: pointer; }}
                </style>
                <script>function imgClick(url) {{ window.external.OnImageClicked(url); }}</script>
                </head><body>{Markdown.ToHtml(readmeContent)}</body></html>";
            html = Regex.Replace(html, "<img ", "<img onclick='imgClick(this.src)' ");
            descriptionBrowser.ObjectForScripting = new BrowserScriptHelper();
            descriptionBrowser.DocumentText = html;
            UseWaitCursor = false;
        }
    }
}