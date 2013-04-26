using System;
using PaganSoft.Aria2.Core;
using PaganSoft.Aria2.Options;
using System.Collections.Generic;

namespace PaganSoft.Aria2
{
    public interface IAria2
    {
        GID AddUri(IEnumerable<Uri> uris, AriaOptions? options = null, int position = -1);
        GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris = null, AriaOptions? options = null, int position = -1);
        GID AddMetalink(byte[] metalink);
        GID AddMetalink(byte[] metalink, AriaOptions options);
        GID AddMetalink(byte[] metalink, AriaOptions options, int position);
        GID Remove(string gid);
        GID ForceRemove(string gid);
        GID Pause(string gid);
        bool PauseAll();
        GID ForcePause(string gid);
        bool ForcePauseAll();
        GID Unpause(string gid);
        bool UnpauseAll();
        StatusInfo TellStatus(string gid);
        StatusInfo TellStatus(string gid, IEnumerable<string> keys);
        IEnumerable<UriStatus> GetUris(string gid);
        IEnumerable<FileInfo> GetFiles(string gid);
        IEnumerable<PeerInfo> GetPeers(string gid);
        IEnumerable<ServersInfo> GetServers(string gid);
        IEnumerable<StatusInfo> TellActive(IEnumerable<string> keys = null);
        IEnumerable<StatusInfo> TellWaiting(int offset, int num, IEnumerable<string> keys = null);
        IEnumerable<StatusInfo> TellStopped(int offset, int num, IEnumerable<string> keys = null);
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
        AriaOptions GetOption(string gid);
        bool ChangeOption(string gid, AriaOptions options);
        AriaOptions GetGlobalOption();
        bool ChangeGlobalOption(AriaOptions options);
        GlobalStats GetGlobalStat();
        bool PurgeDownloadResult();
        bool RemoveDownloadResult(string gid);
        VersionInfo GetVersion();
        string GetSessionId();
        bool Shutdown();
        bool ForceShutdown();
    }
}

