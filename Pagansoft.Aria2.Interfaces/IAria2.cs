using System;
using Pagansoft.Aria2.Core;
using Pagansoft.Aria2.Options;
using System.Collections.Generic;

namespace Pagansoft.Aria2
{
    public interface IAria2
    {
        GID AddUri(IEnumerable<Uri> uris, IAriaOptions options = null, int position = -1);

        GID AddTorrent(byte[] torrent, IEnumerable<Uri> uris = null, IAriaOptions options = null, int position = -1);

        GID AddMetalink(byte[] metalink);

        GID AddMetalink(byte[] metalink, IAriaOptions options);

        GID AddMetalink(byte[] metalink, IAriaOptions options, int position);

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

        IEnumerable<IStatusInfo> TellActive(IEnumerable<string> keys = null);

        IEnumerable<IStatusInfo> TellWaiting(int offset, int num, IEnumerable<string> keys = null);

        IEnumerable<IStatusInfo> TellStopped(int offset, int num, IEnumerable<string> keys = null);

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

        IAriaOptions GetOption(string gid);

        bool ChangeOption(string gid, IAriaOptions options);

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

