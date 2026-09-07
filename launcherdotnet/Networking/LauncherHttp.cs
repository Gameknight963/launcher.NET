using launcherdotnet.Launcher.Settings;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace launcherdotnet.Networking
{
    public static class LauncherHttp
    {
        static readonly SocketsHttpHandler _handler = new();
        public static readonly HttpClient Client;

        static LauncherHttp()
        {
            Client = new HttpClient(_handler);
            Client.DefaultRequestHeaders.Add("User-Agent", $"launcherdotnet/{LauncherConstants.CurrentVersion}");

            if (!LauncherSettings.Settings.DisableIPv6 || !NetworkInterface.GetIsNetworkAvailable()) return;
            _handler.ConnectCallback = async (context, cancellationToken) =>
            {
                IPAddress[] addresses = await Dns.GetHostAddressesAsync(context.DnsEndPoint.Host, cancellationToken);

                IPAddress? ipv4 = addresses.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork) 
                    ?? throw new Exception("No IPv4 address found.");
                Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Why do we use try catch instead of try finally here?
                // Try catch ony executes this on ERROR, try finally does it
                // no matter what.
                try
                {
                    await socket.ConnectAsync(ipv4, context.DnsEndPoint.Port, cancellationToken);
                    return new NetworkStream(socket, ownsSocket: true);
                }
                catch
                {
                    socket.Dispose();
                    throw;
                }
            };
            _ = TestAsync();
        }

        public static async Task TestAsync()
        {
            try
            {
                string host = "www.google.com";
                Stopwatch dnsTimer = Stopwatch.StartNew();
                _ = await Dns.GetHostAddressesAsync(host);
                dnsTimer.Stop();
                LauncherLogger.WriteLine($"DNS lookup: {dnsTimer.ElapsedMilliseconds}ms");
                Stopwatch httpTimer = Stopwatch.StartNew();
                _ = await Client.GetAsync($"https://{host}/generate_204");
                httpTimer.Stop();
                LauncherLogger.WriteLine($"HTTP request: {httpTimer.ElapsedMilliseconds}ms");
            }
            catch (Exception ex)
            {
                LauncherLogger.Warn($"Network test failed: {ex.Message}");
            }
        }
    }
}
