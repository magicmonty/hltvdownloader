using System;
using System.Collections.Generic;
using CookComputing.XmlRpc;
using Pagansoft.Aria2.Core;
using Pagansoft.Aria2.Options;

namespace Pagansoft.Aria2
{
    public interface IAria2
    {
        bool IsRunning { get; }

        bool Start();

        GID AddUri(IEnumerable<Uri> uris);

        GID AddUri(IEnumerable<Uri> uris, XmlRpcStruct options);

        GID AddUri(IEnumerable<Uri> uris, IDictionary<string, string> options, int position);

        GID AddTorrent(byte[] torrent);

        GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris);

        GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris, IDictionary<string, string> options);

        GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris, IDictionary<string, string> options, int position);

        GID AddMetalink(byte[] metalink);

        GID AddMetalink(byte[] metalink, IDictionary<string, string> options);

        GID AddMetalink(byte[] metalink, IDictionary<string, string> options, int position);

        GID Remove(string gid);

        GID ForceRemove(string gid);

        GID Pause(string gid);

        bool PauseAll();

        GID ForcePause(string gid);

        bool ForcePauseAll();

        GID Unpause(string gid);

        bool UnpauseAll();

        IStatusInfo TellStatus(string gid);

        IStatusInfo TellStatus(string gid, IEnumerable<string> keys);

        IEnumerable<IUriStatus> GetUris(string gid);

        IEnumerable<IFileInfo> GetFiles(string gid);

        IEnumerable<IPeerInfo> GetPeers(string gid);

        IEnumerable<IServersInfo> GetServers(string gid);

        IEnumerable<IStatusInfo> TellActive();

        IEnumerable<IStatusInfo> TellActive(IEnumerable<string> keys);

        IEnumerable<IStatusInfo> TellWaiting(int offset, int num);

        IEnumerable<IStatusInfo> TellWaiting(int offset, int num, IEnumerable<string> keys);

        IEnumerable<IStatusInfo> TellStopped(int offset, int num);

        IEnumerable<IStatusInfo> TellStopped(int offset, int num, IEnumerable<string> keys);

        int ChangePosition(string gid, int pos, string how);

        IEnumerable<int> ChangeUri(string gid, 
                                   int fileIndex, 
                                   IEnumerable<string> delUris, 
                                   IEnumerable<string> addUris);

        int[] ChangeUri(string gid, 
                        int fileIndex, 
                        IEnumerable<string> delUris, 
                        IEnumerable<string> addUris, 
                        int position);

        IDictionary<string, string> GetOption(string gid);

        bool ChangeOption(string gid, IDictionary<string, string> options);

        IAriaOptions GetGlobalOption();

        bool ChangeGlobalOption(IAriaOptions options);

        IGlobalStats GetGlobalStat();

        bool PurgeDownloadResult();

        bool RemoveDownloadResult(string gid);

        IVersionInfo GetVersion();

        string GetSessionId();

        bool Shutdown();

        bool ForceShutdown();
    }
}

