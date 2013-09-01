using System;
using Pagansoft.Aria2.XmlRpc;
using CookComputing.XmlRpc;
using Pagansoft.Aria2.Core;
using Pagansoft.Aria2.Options;
using System.Collections.Generic;
using System.Linq;

namespace Pagansoft.Aria2
{
    public class Aria2 : IAria2
    {
        private IAria2c proxy;

        public Aria2()
        {
            proxy = XmlRpcProxyGen.Create<IAria2c>();
        }
        #region IAria2 implementation
        public GID AddUri(IEnumerable<Uri> uris)
        {
            return proxy.AddUri(uris.Select(u => u.ToString()).ToArray());
        }

        public GID AddUri(IEnumerable<Uri> uris, IAriaOptions options)
        {
            return proxy.AddUri(uris.Select(u => u.ToString()).ToArray(), XmlRpc.Options.From(options));
        }

        public GID AddUri(IEnumerable<Uri> uris, IAriaOptions options, int position)
        {
            return proxy.AddUri(uris.Select(u => u.ToString()).ToArray(), XmlRpc.Options.From(options), position);
        }

        public GID AddTorrent(byte[] torrent)
        {
            return proxy.AddTorrent(torrent);
        }

        public GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris)
        {
            return proxy.AddTorrent(torrent, uris.Select(u => u.ToString()).ToArray());
        }

        public GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris, IAriaOptions options)
        {
            return proxy.AddTorrent(torrent, uris.Select(u => u.ToString()).ToArray(), XmlRpc.Options.From(options));
        }

        public GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris, IAriaOptions options, int position)
        {
            return proxy.AddTorrent(torrent, uris.Select(u => u.ToString()).ToArray(), XmlRpc.Options.From(options), position);
        }

        public GID AddMetalink(byte[] metalink)
        {
            return proxy.AddMetalink(metalink);
        }

        public GID AddMetalink(byte[] metalink, IAriaOptions options)
        {
            return proxy.AddMetalink(metalink, XmlRpc.Options.From(options));
        }

        public GID AddMetalink(byte[] metalink, IAriaOptions options, int position)
        {
            return proxy.AddMetalink(metalink, XmlRpc.Options.From(options), position);
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
            return proxy.GetFiles(gid).Select(FileInfo.From);
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

        public bool ChangeOption(string gid, IAriaOptions options)
        {
            return proxy.ChangeOption(gid, XmlRpc.Options.From(options)) == "OK";
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
        #endregion
    }
}

