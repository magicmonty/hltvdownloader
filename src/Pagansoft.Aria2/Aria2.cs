using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Pagansoft.Aria2.Core;
using Pagansoft.Aria2.Options;
using Pagansoft.Aria2.XmlRpc;
using Pagansoft.Homeload.Core;
using Pagansoft.Logging;
using XmlRpcLight.DataTypes;

namespace Pagansoft.Aria2
{
    [Export(typeof(IAria2))]
    public class Aria2 : IAria2
    {
        private readonly IAria2c _proxy;
        private const string ProcessName = "aria2c";
        private readonly IConfiguration _configuration;

        [Import]
        private ILogger _logger;

        [ImportingConstructor]
        public Aria2(IConfiguration configuration)
        {
            _configuration = configuration;
            _proxy = new AriaClient();
        }

        public bool IsRunning => Process.GetProcessesByName(ProcessName).Any();

        public async Task<bool> Start()
        {
            if (IsRunning)
            {
                _logger.LogDebug("Aria is already running!");
                return true;
            }

            try
            {
                var psInfo = new ProcessStartInfo
                {
                    FileName = FindExePath(ProcessName),
                    CreateNoWindow = false
                };

                var sessionFile = Path.Combine(_configuration.ConfigurationDirectory, "session.aria");
                var ownPath = AppDomain.CurrentDomain.BaseDirectory;
                var downloadsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                var ariaLogFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".hltc", "aria.log");
                var downloadCompleteCommand = Path.Combine(ownPath, "hltvcomplete");
                var downloadErrorCommand = Path.Combine(ownPath, "hltverror");

                var arguments = new [] 
                {
                    File.Exists(sessionFile) ? $"--input-file={sessionFile}" : "",
                    "--enable-rpc",
                    "--rpc-listen-all",
                    "--rpc-listen-port=6800",
                    "--retry-wait=30",
                    "--pause",
                    $"--dir={downloadsFolder}",
                    "--check-integrity=true", // Check integrity
                    "--split=1", // use only one connection per file
                    "--max-concurrent-downloads=5",
                    "--max-connection-per-server=5",
                    "--log-level=notice",
                    "--show-console-readout=false",
                    "--no-conf=true",
                    "--quiet",
                    $"--log={ariaLogFile}",
                    $"--save-session={sessionFile}",
                    $"--on-download-complete={downloadCompleteCommand}",
                    $"--on-download-error={downloadErrorCommand}",
                };
                
                psInfo.Arguments = string.Join(" ", arguments).Trim();

                try
                {
                    await Task.Run(() =>
                    {
                        _logger.LogInfo("Starting aria2c");
                        Process.Start(psInfo);
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Could not start aria2c!");
                    return false;
                }

                await Task.Delay(TimeSpan.FromMilliseconds(500));

                return IsRunning;
            }
            catch (FileNotFoundException)
            {
                _logger.LogError("Could not find aria2c in PATH");
                return false;
            }
        }

        /// <summary>
        /// Expands environment variables and, if unqualified, locates the exe in the working directory
        /// or the evironment's path.
        /// </summary>
        /// <param name="exe">The name of the executable file</param>
        /// <returns>The fully-qualified path to the file</returns>
        /// <exception cref="System.IO.FileNotFoundException">Raised when the exe was not found</exception>
        public static string FindExePath(string exe)
        {
            exe = Environment.ExpandEnvironmentVariables(exe);
            var pathVar = Environment.GetEnvironmentVariable("PATH") ?? "";

            if (File.Exists(exe))
                return Path.GetFullPath(exe);
            if (Path.GetDirectoryName(exe) != string.Empty)
                throw new FileNotFoundException(new FileNotFoundException().Message, exe);

            foreach (var p in pathVar.Split(Path.PathSeparator).Select(p => p.Trim()))
            {
                var path = p;
                if (!string.IsNullOrEmpty(path) && File.Exists(path = Path.Combine(path, exe)))
                    return Path.GetFullPath(path);
            }

            throw new FileNotFoundException(new FileNotFoundException().Message, exe);
        }

        public async Task<GID> AddUri(IEnumerable<Uri> uris)
        {
            return await _proxy.AddUri(uris.Select(u => u.ToString()).ToArray());
        }

        public async Task<GID> AddUri(IEnumerable<Uri> uris, XmlRpcStruct options)
        {
            return await _proxy.AddUri(uris.Select(u => u.ToString()).ToArray(), options);
        }

        public async Task<GID> AddUri(IEnumerable<Uri> uris, IDictionary<string, string> options, int position)
        {
            return await _proxy.AddUri(uris.Select(u => u.ToString()).ToArray(), options, position);
        }

        public async Task<GID> AddTorrent(byte[] torrent)
        {
            return await _proxy.AddTorrent(torrent);
        }

        public async Task<GID> AddTorrent(byte[] torrent, IEnumerable<Uri> uris)
        {
            return await _proxy.AddTorrent(torrent, uris.Select(u => u.ToString()).ToArray());
        }

        public async Task<GID> AddTorrent(byte[] torrent, IEnumerable<Uri> uris, IDictionary<string, string> options)
        {
            return await _proxy.AddTorrent(torrent, uris.Select(u => u.ToString()).ToArray(), options);
        }

        public async Task<GID> AddTorrent(byte[] torrent, IEnumerable<Uri> uris, IDictionary<string, string> options, int position)
        {
            return await _proxy.AddTorrent(torrent, uris.Select(u => u.ToString()).ToArray(), options, position);
        }

        public async Task<GID> AddMetalink(byte[] metalink)
        {
            return await _proxy.AddMetalink(metalink);
        }

        public async Task<GID> AddMetalink(byte[] metalink, IDictionary<string, string> options)
        {
            return await _proxy.AddMetalink(metalink, options);
        }

        public async Task<GID> AddMetalink(byte[] metalink, IDictionary<string, string> options, int position)
        {
            return await _proxy.AddMetalink(metalink, options, position);
        }

        public async Task<GID> Remove(string gid)
        {
            return await _proxy.Remove(gid);
        }

        public async Task<GID> ForceRemove(string gid)
        {
            return await _proxy.ForceRemove(gid);
        }

        public async Task<GID> Pause(string gid)
        {
            return await _proxy.Pause(gid);
        }

        public async Task<bool> PauseAll()
        {
            var response = await _proxy.PauseAll();
            bool result;
            return bool.TryParse(response, out result) && result;
        }

        public async Task<GID> ForcePause(string gid)
        {
            return await _proxy.ForcePause(gid);
        }

        public async Task<bool> ForcePauseAll()
        {
            var response = await _proxy.ForcePauseAll();
            bool result;
            return bool.TryParse(response, out result) && result;
        }

        public async Task<GID> Unpause(string gid)
        {
            return await _proxy.Unpause(gid);
        }

        public async Task<bool> UnpauseAll()
        {
            var response = await _proxy.UnpauseAll();
            bool result;
            return bool.TryParse(response, out result) && result;
        }

        public async Task<IStatusInfo> TellStatus(string gid)
        {
            var response = await _proxy.TellStatus(gid);
            return StatusInfo.From(response);
        }

        public async Task<IStatusInfo> TellStatus(string gid, IEnumerable<string> keys)
        {
            var response = await _proxy.TellStatus(gid, keys.ToArray());
            return StatusInfo.From(response);
        }

        public async Task<IEnumerable<IUriStatus>> GetUris(string gid)
        {
            var response = await _proxy.GetUris(gid);
            return response.Select(UriStatus.From);
        }

        public async Task<IEnumerable<IFileInfo>> GetFiles(string gid)
        {
            var response = await _proxy.GetFiles(gid);
            return response.Select(Pagansoft.Aria2.Core.FileInfo.From);
        }

        public async Task<IEnumerable<IPeerInfo>> GetPeers(string gid)
        {
            var response = await _proxy.GetPeers(gid);
            return response.Select(PeerInfo.From);
        }

        public async Task<IEnumerable<IServersInfo>> GetServers(string gid)
        {
            var response = await _proxy.GetServers(gid);
            return response.Select(ServersInfo.From);
        }

        public async Task<IEnumerable<IStatusInfo>> TellActive()
        {
            var response = await _proxy.TellActive();
            return response.Select(StatusInfo.From);
        }

        public async Task<IEnumerable<IStatusInfo>> TellActive(IEnumerable<string> keys)
        {
            var response = await _proxy.TellActive(keys.ToArray());
            return response.Select(StatusInfo.From);
        }

        public async Task<IEnumerable<IStatusInfo>> TellWaiting(int offset, int num)
        {
            var response = await _proxy.TellWaiting(offset, num);
            return response.Select(StatusInfo.From);
        }

        public async Task<IEnumerable<IStatusInfo>> TellWaiting(int offset, int num, IEnumerable<string> keys)
        {
            var response = await _proxy.TellWaiting(offset, num, keys.ToArray());
            return response.Select(StatusInfo.From);
        }

        public async Task<IEnumerable<IStatusInfo>> TellStopped(int offset, int num)
        {
            var response = await _proxy.TellStopped(offset, num);
            return response.Select(StatusInfo.From);
        }

        public async Task<IEnumerable<IStatusInfo>> TellStopped(int offset, int num, IEnumerable<string> keys)
        {
            var response = await _proxy.TellStopped(offset, num, keys.ToArray());
            return response.Select(StatusInfo.From);
        }

        public async Task<int> ChangePosition(string gid, int pos, string how)
        {
            return await _proxy.ChangePosition(gid, pos, how);
        }

        public async Task<IEnumerable<int>> ChangeUri(string gid, int fileIndex, IEnumerable<string> delUris, IEnumerable<string> addUris)
        {
            return await _proxy.ChangeUri(gid, fileIndex, delUris.ToArray(), addUris.ToArray());
        }

        public async Task<int[]> ChangeUri(string gid, int fileIndex, IEnumerable<string> delUris, IEnumerable<string> addUris, int position)
        {
            return await _proxy.ChangeUri(gid, fileIndex, delUris.ToArray(), addUris.ToArray(), position);
        }

        public async Task<IDictionary<string, string>> GetOption(string gid)
        {
            return await _proxy.GetOption(gid);
        }

        public async Task<bool> ChangeOption(string gid, IDictionary<string, string> options)
        {
            var response = await _proxy.ChangeOption(gid, options);
            return response == "OK";
        }

        public async Task<IAriaOptions> GetGlobalOption()
        {
            var response = await _proxy.GetGlobalOption();
            return AriaOptions.From(response);
        }

        public async Task<bool> ChangeGlobalOption(IAriaOptions options)
        {
            var response = await _proxy.ChangeGlobalOption(XmlRpc.Options.From(options));
            return response == "OK";
        }

        public async Task<IGlobalStats> GetGlobalStat()
        {
            var response = await _proxy.GetGlobalStat();
            return GlobalStats.From(response);
        }

        public async Task<bool> PurgeDownloadResult()
        {
            var response = await _proxy.PurgeDownloadResult();
            return response == "OK";
        }

        public async Task<bool> RemoveDownloadResult(string gid)
        {
            var response = await _proxy.RemoveDownloadResult(gid);
            return response == "OK";
        }

        public async Task<IVersionInfo> GetVersion()
        {
            var response = await _proxy.GetVersion();
            return VersionInfo.From(response);
        }

        public async Task<string> GetSessionId()
        {
            var response = await _proxy.GetSessionInfo();
            return response.SessionId;
        }

        public async Task<bool> Shutdown()
        {
            var response = await _proxy.Shutdown();
            return response == "OK";
        }

        public async Task<bool> ForceShutdown()
        {
            var response = await _proxy.ForceShutdown();
            return response == "OK";
        }
    }
}

