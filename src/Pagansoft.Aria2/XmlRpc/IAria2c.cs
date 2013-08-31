using System;
using System.Collections.Generic;
using CookComputing.XmlRpc;

namespace Pagansoft.Aria2.XmlRpc
{
    [XmlRpcUrl("http://localhost:6800/rpc")]
    public interface IAria2c : IXmlRpcProxy
    {
        [XmlRpcMethod("aria2.addUri")]
        string AddUri(Uri[] uris);

        [XmlRpcMethod("aria2.addUri")]
        string AddUri(Uri[] uris, Options options);

        [XmlRpcMethod("aria2.addUri")]
        string AddUri(Uri[] uris, Options options, int position);

        [XmlRpcMethod("aria2.addTorrent")]
        string AddTorrent(byte[] torrent);

        [XmlRpcMethod("aria2.addTorrent")]
        string AddTorrent(byte[] torrent, string[] uris);

        [XmlRpcMethod("aria2.addTorrent")]
        string AddTorrent(byte[] torrent, string[] uris, Options options);

        [XmlRpcMethod("aria2.addTorrent")]
        string AddTorrent(byte[] torrent, string[] uris, Options options, int position);

        [XmlRpcMethod("aria2.addMetalink")]
        string AddMetalink(byte[] metalink);

        [XmlRpcMethod("aria2.addMetalink")]
        string AddMetalink(byte[] metalink, Options options);

        [XmlRpcMethod("aria2.addMetalink")]
        string AddMetalink(byte[] metalink, Options options, int position);

        [XmlRpcMethod("aria2.remove")]
        string Remove(string gid);

        [XmlRpcMethod("aria2.forceRemove")]
        string ForceRemove(string gid);

        [XmlRpcMethod("aria2.pause")]
        string Pause(string gid);

        [XmlRpcMethod("aria2.pauseAll")]
        string PauseAll();

        [XmlRpcMethod("aria2.forcePause")]
        string ForcePause(string gid);

        [XmlRpcMethod("aria2.forcePauseAll")]
        string ForcePauseAll();

        [XmlRpcMethod("aria2.unpause")]
        string Unpause(string gid);

        [XmlRpcMethod("aria2.unpauseAll")]
        string UnpauseAll();

        [XmlRpcMethod("aria2.tellStatus")]
        StatusResponse TellStatus(string gid);

        [XmlRpcMethod("aria2.tellStatus")]
        StatusResponse TellStatus(string gid, string[] keys);

        [XmlRpcMethod("aria2.getUris")]
        UriResponse[] GetUris(string gid);

        [XmlRpcMethod("aria2.getFiles")]
        FileResponse[] GetFiles(string gid);

        [XmlRpcMethod("aria2.getPeers")]
        PeerResponse[] GetPeers(string gid);

        [XmlRpcMethod("aria2.getServers")]
        ServersResponse[] GetServers(string gid);

        [XmlRpcMethod("aria2.tellActive")]
        StatusResponse[] TellActive();

        [XmlRpcMethod("aria2.tellActive")]
        StatusResponse[] TellActive(string[] keys);

        [XmlRpcMethod("aria2.tellWaiting")]
        StatusResponse[] TellWaiting(int offset, int num);

        [XmlRpcMethod("aria2.tellWaiting")]
        StatusResponse[] TellWaiting(int offset, int num, string[] keys);

        [XmlRpcMethod("aria2.tellStopped")]
        StatusResponse[] TellStopped(int offset, int num);

        [XmlRpcMethod("aria2.tellStopped")]
        StatusResponse[] TellStopped(int offset, int num, string[] keys);

        [XmlRpcMethod("aria2.changePosition")]
        int ChangePosition(string gid, int pos, string how);

        [XmlRpcMethod("aria2.changeUri")]
        int[] ChangeUri(string gid, int fileIndex, string[] delUris, string[] addUris);

        [XmlRpcMethod("aria2.changeUri")]
        int[] ChangeUri(string gid, int fileIndex, string[] delUris, string[] addUris, int position);

        [XmlRpcMethod("aria2.getOption")]
        IDictionary<string, string> GetOption(string gid);

        [XmlRpcMethod("aria2.changeOption")]
        string ChangeOption(string gid, Options options);

        [XmlRpcMethod("aria2.getGlobalOption")]
        Options GetGlobalOption();

        [XmlRpcMethod("aria2.changeGlobalOption")]
        string ChangeGlobalOption(Options options);

        [XmlRpcMethod("aria2.getGlobalStat")]
        GlobalStatResponse GetGlobalStat();

        [XmlRpcMethod("aria2.purgeDownloadResult")]
        string PurgeDownloadResult();

        [XmlRpcMethod("aria2.removeDownloadResult")]
        string RemoveDownloadResult(string gid);

        [XmlRpcMethod("aria2.getVersion")]
        VersionResponse GetVersion();

        [XmlRpcMethod("aria2.getSessionInfo")]
        SessionInfoResponse GetSessionInfo();

        [XmlRpcMethod("aria2.shutdown")]
        string Shutdown();

        [XmlRpcMethod("aria2.forceShutdown")]
        string ForceShutdown();
    }
}
