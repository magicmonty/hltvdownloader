using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CookComputing.XmlRpc;
using Pagansoft.Aria2.Core;
using Pagansoft.Aria2.Options;
using Pagansoft.Aria2.XmlRpc;
using Pagansoft.Homeload.Core;

namespace Pagansoft.Aria2
{
    [Export(typeof(IAria2))]
    public class Aria2 : IAria2
    {
        private IAria2c proxy;
        private const string ProcessName = "aria2c";
        private IConfiguration _configuration;

        [ImportingConstructor]
        public Aria2(IConfiguration configuration)
        {
            proxy = XmlRpcProxyGen.Create<IAria2c>();
            _configuration = configuration;
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
                var ownPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HLTVDownloader.exe");

                if (File.Exists(sessionFile))
                    arguments.Add("--input-file" + sessionFile);

                arguments.Add("--enable-rpc");
                arguments.Add("--rpc-listen-all");
                arguments.Add("--rpc-listen-port=6800");
                arguments.Add("--retry-wait=30");
                arguments.Add("--pause");
                arguments.Add("--dir=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"));
                arguments.Add("--check-integrity=true"); // Check integrity
                arguments.Add("--split=1"); // use only one connection per file
                arguments.Add("--max-concurrent-downloads=10");
                arguments.Add("--max-connection-per-server=10");
                arguments.Add("--save-session=" + sessionFile);
                arguments.Add(@"--on-download-complete=""" + ownPath + @" --completed""");
                arguments.Add(@"--on-download-error=""" + ownPath + @" --error""");

                psInfo.Arguments = string.Join(" ", arguments);
                
                Task.Factory.StartNew(() =>
                {
                    var process = Process.Start(psInfo);
                    process.WaitForExit();
                });

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

        public GID AddUri(IEnumerable<Uri> uris)
        {
            return proxy.AddUri(uris.Select(u => u.ToString()).ToArray());
        }

        public GID AddUri(IEnumerable<Uri> uris, XmlRpcStruct options)
        {
            return proxy.AddUri(uris.Select(u => u.ToString()).ToArray(), options);
        }

        public GID AddUri(IEnumerable<Uri> uris, IDictionary<string, string> options, int position)
        {
            return proxy.AddUri(uris.Select(u => u.ToString()).ToArray(), options, position);
        }

        public GID AddTorrent(byte[] torrent)
        {
            return proxy.AddTorrent(torrent);
        }

        public GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris)
        {
            return proxy.AddTorrent(torrent, uris.Select(u => u.ToString()).ToArray());
        }

        public GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris, IDictionary<string, string> options)
        {
            return proxy.AddTorrent(torrent, uris.Select(u => u.ToString()).ToArray(), options);
        }

        public GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris, IDictionary<string, string> options, int position)
        {
            return proxy.AddTorrent(torrent, uris.Select(u => u.ToString()).ToArray(), options, position);
        }

        public GID AddMetalink(byte[] metalink)
        {
            return proxy.AddMetalink(metalink);
        }

        public GID AddMetalink(byte[] metalink, IDictionary<string, string> options)
        {
            return proxy.AddMetalink(metalink, options);
        }

        public GID AddMetalink(byte[] metalink, IDictionary<string, string> options, int position)
        {
            return proxy.AddMetalink(metalink, options, position);
        }

        public GID Remove(string gid)
        {
            return proxy.Remove(gid);
        }

        public GID ForceRemove(string gid)
        {
            return proxy.ForceRemove(gid);
        }

        public GID Pause(string gid)
        {
            return proxy.Pause(gid);
        }

        public bool PauseAll()
        {
            bool result;
            if (bool.TryParse(proxy.PauseAll(), out result))
                return result;

            return false;
        }

        public GID ForcePause(string gid)
        {
            return proxy.ForcePause(gid);
        }

        public bool ForcePauseAll()
        {
            bool result;
            if (bool.TryParse(proxy.ForcePauseAll(), out result))
                return result;

            return false;
        }

        public GID Unpause(string gid)
        {
            return proxy.Unpause(gid);
        }

        public bool UnpauseAll()
        {
            bool result;
            if (bool.TryParse(proxy.UnpauseAll(), out result))
                return result;

            return false;
        }

        public IStatusInfo TellStatus(string gid)
        {
            return StatusInfo.From(proxy.TellStatus(gid));
        }

        public IStatusInfo TellStatus(string gid, IEnumerable<string> keys)
        {
            return StatusInfo.From(proxy.TellStatus(gid, keys.ToArray()));
        }

        public IEnumerable<IUriStatus> GetUris(string gid)
        {
            return proxy.GetUris(gid).Select(UriStatus.From);
        }

        public IEnumerable<IFileInfo> GetFiles(string gid)
        {
            return proxy.GetFiles(gid).Select(Pagansoft.Aria2.Core.FileInfo.From);
        }

        public IEnumerable<IPeerInfo> GetPeers(string gid)
        {
            return proxy.GetPeers(gid).Select(PeerInfo.From);
        }

        public IEnumerable<IServersInfo> GetServers(string gid)
        {
            return proxy.GetServers(gid).Select(ServersInfo.From);
        }

        public IEnumerable<IStatusInfo> TellActive()
        {
            return proxy.TellActive().Select(StatusInfo.From);
        }

        public IEnumerable<IStatusInfo> TellActive(IEnumerable<string> keys)
        {
            return proxy.TellActive(keys.ToArray()).Select(StatusInfo.From);
        }

        public IEnumerable<IStatusInfo> TellWaiting(int offset, int num)
        {
            return proxy.TellWaiting(offset, num).Select(StatusInfo.From);
        }

        public IEnumerable<IStatusInfo> TellWaiting(int offset, int num, IEnumerable<string> keys)
        {
            return proxy.TellWaiting(offset, num, keys.ToArray()).Select(StatusInfo.From);
        }

        public IEnumerable<IStatusInfo> TellStopped(int offset, int num)
        {
            return proxy.TellStopped(offset, num).Select(StatusInfo.From);
        }

        public IEnumerable<IStatusInfo> TellStopped(int offset, int num, IEnumerable<string> keys)
        {
            return proxy.TellStopped(offset, num, keys.ToArray()).Select(StatusInfo.From);
        }

        public int ChangePosition(string gid, int pos, string how)
        {
            return proxy.ChangePosition(gid, pos, how);
        }

        public IEnumerable<int> ChangeUri(string gid, int fileIndex, IEnumerable<string> delUris, IEnumerable<string> addUris)
        {
            return proxy.ChangeUri(gid, fileIndex, delUris.ToArray(), addUris.ToArray());
        }

        public int[] ChangeUri(string gid, int fileIndex, IEnumerable<string> delUris, IEnumerable<string> addUris, int position)
        {
            return proxy.ChangeUri(gid, fileIndex, delUris.ToArray(), addUris.ToArray(), position);
        }

        public IDictionary<string, string> GetOption(string gid)
        {
            return proxy.GetOption(gid);
        }

        public bool ChangeOption(string gid, IDictionary<string, string> options)
        {
            return proxy.ChangeOption(gid, options) == "OK";
        }

        public IAriaOptions GetGlobalOption()
        {
            return AriaOptions.From(proxy.GetGlobalOption());
        }

        public bool ChangeGlobalOption(IAriaOptions options)
        {
            return proxy.ChangeGlobalOption(XmlRpc.Options.From(options)) == "OK";
        }

        public IGlobalStats GetGlobalStat()
        {
            return GlobalStats.From(proxy.GetGlobalStat());
        }

        public bool PurgeDownloadResult()
        {
            return proxy.PurgeDownloadResult() == "OK";
        }

        public bool RemoveDownloadResult(string gid)
        {
            return proxy.RemoveDownloadResult(gid) == "OK";
        }

        public IVersionInfo GetVersion()
        {
            return VersionInfo.From(proxy.GetVersion());
        }

        public string GetSessionId()
        {
            return proxy.GetSessionInfo().SessionId;
        }

        public bool Shutdown()
        {
            return proxy.Shutdown() == "OK";
        }

        public bool ForceShutdown()
        {
            return proxy.ForceShutdown() == "OK";
        }
    }
}

