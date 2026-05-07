using launcherdotnet.Styling;
using launcherdotnet.Thunderstore;
using Markdig;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using Svg;

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

        private static readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .Build();

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

            descriptionPanel.ImageLoad += async (sender, args) =>
            {
                if (Path.GetExtension(args.Src)?.Equals(".svg", StringComparison.OrdinalIgnoreCase) == true
                    || args.Src.Contains("shields.io"))
                {
                    args.Handled = true;
                    byte[] data = await ThunderstoreClient.Http.GetByteArrayAsync(args.Src);
                    using MemoryStream ms = new(data);
                    SvgDocument svgDoc = SvgDocument.Open<SvgDocument>(ms);
                    Bitmap svgImg = new((int)svgDoc.Width, (int)svgDoc.Height, PixelFormat.Format32bppArgb);
                    svgDoc.Draw(svgImg);
                    args.Callback(svgImg);
                }
            };
            descriptionPanel.LinkClicked += (sender, e) =>
            {
                LauncherLogger.WriteLine($"LinkClicked: {e.Link}");
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
            UpdateBrowserReadme(readmeContent);
            UseWaitCursor = false;
        }

        private void UpdateBrowserReadme(string readmeContent)
        {
            _currentReadme = readmeContent;
            string fg = ColorTranslator.ToHtml(ForeColor);
            string raisedBg = ColorTranslator.ToHtml(ControlPaint.Light(BackColor, 0.1f));
            string body = Markdown.ToHtml(_currentReadme, _pipeline);
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
            if (_currentReadme != null) UpdateBrowserReadme(_currentReadme);
        }
    }
}