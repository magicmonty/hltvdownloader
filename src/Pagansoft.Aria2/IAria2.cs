using System;
using System.Collections.Generic;
using XmlRpcLight.DataTypes;
using XmlRpcLight.Attributes;
using Pagansoft.Aria2.Core;
using Pagansoft.Aria2.Options;
using System.Threading.Tasks;

namespace Pagansoft.Aria2
{
    public interface IAria2
    {
        bool IsRunning { get; }

        bool Start();

        Task<GID> AddUri(IEnumerable<Uri> uris);

        Task<GID> AddUri(IEnumerable<Uri> uris, XmlRpcStruct options);

        Task<GID> AddUri(IEnumerable<Uri> uris, IDictionary<string, string> options, int position);

        Task<GID> AddTorrent(byte[] torrent);

        Task<GID> AddTorrent(byte[] torrent, IEnumerable<Uri> uris);

        Task<GID> AddTorrent(byte[] torrent, IEnumerable<Uri> uris, IDictionary<string, string> options);

        Task<GID> AddTorrent(byte[] torrent, IEnumerable<Uri> uris, IDictionary<string, string> options, int position);

        Task<GID> AddMetalink(byte[] metalink);

        Task<GID> AddMetalink(byte[] metalink, IDictionary<string, string> options);

        Task<GID> AddMetalink(byte[] metalink, IDictionary<string, string> options, int position);

        Task<GID> Remove(string gid);

        Task<GID> ForceRemove(string gid);

        Task<GID> Pause(string gid);

        Task<bool> PauseAll();

        Task<GID> ForcePause(string gid);

        Task<bool> ForcePauseAll();

        Task<GID> Unpause(string gid);

        Task<bool> UnpauseAll();

        Task<IStatusInfo> TellStatus(string gid);

        Task<IStatusInfo> TellStatus(string gid, IEnumerable<string> keys);

        Task<IEnumerable<IUriStatus>> GetUris(string gid);

        Task<IEnumerable<IFileInfo>> GetFiles(string gid);

        Task<IEnumerable<IPeerInfo>> GetPeers(string gid);

        Task<IEnumerable<IServersInfo>> GetServers(string gid);

        Task<IEnumerable<IStatusInfo>> TellActive();

        Task<IEnumerable<IStatusInfo>> TellActive(IEnumerable<string> keys);

        Task<IEnumerable<IStatusInfo>> TellWaiting(int offset, int num);

        Task<IEnumerable<IStatusInfo>> TellWaiting(int offset, int num, IEnumerable<string> keys);

        Task<IEnumerable<IStatusInfo>> TellStopped(int offset, int num);

        Task<IEnumerable<IStatusInfo>> TellStopped(int offset, int num, IEnumerable<string> keys);

        Task<int> ChangePosition(string gid, int pos, string how);

        Task<IEnumerable<int>> ChangeUri(string gid, 
                                   int fileIndex, 
                                   IEnumerable<string> delUris, 
                                   IEnumerable<string> addUris);

        Task<int[]> ChangeUri(string gid, 
                        int fileIndex, 
                        IEnumerable<string> delUris, 
                        IEnumerable<string> addUris, 
                        int position);

        Task<IDictionary<string, string>> GetOption(string gid);

        Task<bool> ChangeOption(string gid, IDictionary<string, string> options);

        Task<IAriaOptions> GetGlobalOption();

        Task<bool> ChangeGlobalOption(IAriaOptions options);

        Task<IGlobalStats> GetGlobalStat();

        Task<bool> PurgeDownloadResult();

        Task<bool> RemoveDownloadResult(string gid);

        Task<IVersionInfo> GetVersion();

        Task<string> GetSessionId();

        Task<bool> Shutdown();

        Task<bool> ForceShutdown();
    }
}

