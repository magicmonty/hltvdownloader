using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XmlRpcLight.DataTypes;
using Pagansoft.Homeload.Core;
using Pagansoft.Aria2.Core;
using Pagansoft.Aria2.Options;
using Pagansoft.Aria2.XmlRpc;
using System.CodeDom;

namespace Pagansoft.Aria2
{
    [Export(typeof(IAria2))]
    public class Aria2 : IAria2
    {
        private readonly IAria2c _proxy;
        const string ProcessName = "aria2c";
        IConfiguration _configuration;

        [ImportingConstructor]
        public Aria2(IConfiguration configuration)
        {
            _configuration = configuration;
            _proxy = new AriaClient();
        }

        public bool IsRunning
        {
            get
            {
                return Process.GetProcessesByName(ProcessName).Any();
            }
        }

        public bool Start()
        {
            if (IsRunning)
                return true;

            try
            {

                var psInfo = new ProcessStartInfo();
                
                psInfo.FileName = FindExePath(ProcessName);
                psInfo.CreateNoWindow = false;
                
                var arguments = new List<string>();

                var sessionFile = Path.Combine(_configuration.ConfigurationDirectory, "session.aria");
                var ownPath = AppDomain.CurrentDomain.BaseDirectory;

                if (File.Exists(sessionFile))
                    arguments.Add("--input-file=" + sessionFile);

                arguments.Add("--enable-rpc");
                arguments.Add("--rpc-listen-all");
                arguments.Add("--rpc-listen-port=6800");
                // arguments.Add("--rpc-secret=12345");
                arguments.Add("--retry-wait=30");
                arguments.Add("--pause");
                arguments.Add("--dir=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"));
                arguments.Add("--check-integrity=true"); // Check integrity
                arguments.Add("--split=1"); // use only one connection per file
                arguments.Add("--max-concurrent-downloads=5");
                arguments.Add("--max-connection-per-server=5");
                arguments.Add("--log-level=notice");
                arguments.Add("--show-console-readout=false");
                arguments.Add("--no-conf=true");
                arguments.Add("--quiet");
                arguments.Add("--log=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".hltc", "aria.log"));
                arguments.Add("--save-session=" + sessionFile);
                arguments.Add(@"--on-download-complete=" + Path.Combine(ownPath, "hltvcomplete"));
                arguments.Add(@"--on-download-error=" + Path.Combine(ownPath, "hltverror"));

                psInfo.Arguments = string.Join(" ", arguments);
                
                var task = Task.Factory.StartNew(() =>
                {
                    var process = Process.Start(psInfo);
                    process.WaitForExit();
                });

                if (task.IsFaulted)
                    return false;

                Thread.Sleep(200);

                return IsRunning;
            }
            catch (FileNotFoundException)
            {
                Console.Out.WriteLine("Could not find aria2c in PATH");
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

            if (!File.Exists(exe))
            {
                if (Path.GetDirectoryName(exe) == String.Empty)
                {
                    foreach (string test in (pathVar.Split(Path.PathSeparator)))
                    {
                        string path = test.Trim();
                        if (!String.IsNullOrEmpty(path) && File.Exists(path = Path.Combine(path, exe)))
                            return Path.GetFullPath(path);
                    }
                }
                throw new FileNotFoundException(new FileNotFoundException().Message, exe);
            }
            return Path.GetFullPath(exe);
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

