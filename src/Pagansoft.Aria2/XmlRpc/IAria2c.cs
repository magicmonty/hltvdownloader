using System.Collections.Generic;
using XmlRpcLight.DataTypes;
using XmlRpcLight.Enums;
using XmlRpcLight.Attributes;
using System.Threading.Tasks;

namespace Pagansoft.Aria2.XmlRpc
{
    [XmlRpcUrl("http://localhost:6800/rpc")]
    public interface IAria2c 
    {
        [XmlRpcMethod("aria2.addUri")]
        Task<string> AddUri(string[] uris);

        [XmlRpcMethod("aria2.addUri")]
        Task<string> AddUri(string[] uris, XmlRpcStruct options);

        [XmlRpcMethod("aria2.addUri")]
        Task<string> AddUri(string[] uris, IDictionary<string, string> options, int position);

        [XmlRpcMethod("aria2.addTorrent")]
        Task<string> AddTorrent(byte[] torrent);

        [XmlRpcMethod("aria2.addTorrent")]
        Task<string> AddTorrent(byte[] torrent, string[] uris);

        [XmlRpcMethod("aria2.addTorrent")]
        Task<string> AddTorrent(byte[] torrent, string[] uris, IDictionary<string, string> options);

        [XmlRpcMethod("aria2.addTorrent")]
        Task<string> AddTorrent(byte[] torrent, string[] uris, IDictionary<string, string> options, int position);

        [XmlRpcMethod("aria2.addMetalink")]
        Task<string> AddMetalink(byte[] metalink);

        [XmlRpcMethod("aria2.addMetalink")]
        Task<string> AddMetalink(byte[] metalink, IDictionary<string, string> options);

        [XmlRpcMethod("aria2.addMetalink")]
        Task<string> AddMetalink(byte[] metalink, IDictionary<string, string> options, int position);

        [XmlRpcMethod("aria2.remove")]
        Task<string> Remove(string gid);

        [XmlRpcMethod("aria2.forceRemove")]
        Task<string> ForceRemove(string gid);

        [XmlRpcMethod("aria2.pause")]
        Task<string> Pause(string gid);

        [XmlRpcMethod("aria2.pauseAll")]
        Task<string> PauseAll();

        [XmlRpcMethod("aria2.forcePause")]
        Task<string> ForcePause(string gid);

        [XmlRpcMethod("aria2.forcePauseAll")]
        Task<string> ForcePauseAll();

        [XmlRpcMethod("aria2.unpause")]
        Task<string> Unpause(string gid);

        [XmlRpcMethod("aria2.unpauseAll")]
        Task<string> UnpauseAll();

        [XmlRpcMethod("aria2.tellStatus")]
        Task<StatusResponse> TellStatus(string gid);

        [XmlRpcMethod("aria2.tellStatus")]
        Task<StatusResponse> TellStatus(string gid, string[] keys);

        [XmlRpcMethod("aria2.getUris")]
        Task<UriResponse[]> GetUris(string gid);

        [XmlRpcMethod("aria2.getFiles")]
        Task<FileResponse[]> GetFiles(string gid);

        [XmlRpcMethod("aria2.getPeers")]
        Task<PeerResponse[]> GetPeers(string gid);

        [XmlRpcMethod("aria2.getServers")]
        Task<ServersResponse[]> GetServers(string gid);

        [XmlRpcMethod("aria2.tellActive")]
        Task<StatusResponse[]> TellActive();

        [XmlRpcMethod("aria2.tellActive")]
        Task<StatusResponse[]> TellActive(string[] keys);

        [XmlRpcMethod("aria2.tellWaiting")]
        Task<StatusResponse[]> TellWaiting(int offset, int num);

        [XmlRpcMethod("aria2.tellWaiting")]
        Task<StatusResponse[]> TellWaiting(int offset, int num, string[] keys);

        [XmlRpcMethod("aria2.tellStopped")]
        Task<StatusResponse[]> TellStopped(int offset, int num);

        [XmlRpcMethod("aria2.tellStopped")]
        Task<StatusResponse[]> TellStopped(int offset, int num, string[] keys);

        [XmlRpcMethod("aria2.changePosition")]
        Task<int> ChangePosition(string gid, int pos, string how);

        [XmlRpcMethod("aria2.changeUri")]
        Task<int[]> ChangeUri(string gid, int fileIndex, string[] delUris, string[] addUris);

        [XmlRpcMethod("aria2.changeUri")]
        Task<int[]> ChangeUri(string gid, int fileIndex, string[] delUris, string[] addUris, int position);

        [XmlRpcMethod("aria2.getOption")]
        Task<IDictionary<string, string>> GetOption(string gid);

        [XmlRpcMethod("aria2.changeOption")]
        Task<string> ChangeOption(string gid, IDictionary<string, string> options);

        [XmlRpcMethod("aria2.getGlobalOption")]
        Task<Options> GetGlobalOption();

        [XmlRpcMethod("aria2.changeGlobalOption")]
        Task<string> ChangeGlobalOption(Options options);

        [XmlRpcMethod("aria2.getGlobalStat")]
        Task<GlobalStatResponse> GetGlobalStat();

        [XmlRpcMethod("aria2.purgeDownloadResult")]
        Task<string> PurgeDownloadResult();

        [XmlRpcMethod("aria2.removeDownloadResult")]
        Task<string> RemoveDownloadResult(string gid);

        [XmlRpcMethod("aria2.getVersion")]
        Task<VersionResponse> GetVersion();

        [XmlRpcMethod("aria2.getSessionInfo")]
        Task<SessionInfoResponse> GetSessionInfo();

        [XmlRpcMethod("aria2.shutdown")]
        Task<string> Shutdown();

        [XmlRpcMethod("aria2.forceShutdown")]
        Task<string> ForceShutdown();
    }
}
