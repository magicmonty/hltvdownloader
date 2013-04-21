using System;

namespace PaganSoft.Aria2
{
    public class GID : String
    {
    };


    public interface IAria2
    {
        GID AddUri(Uri[] uris, Options options = null, int position = -1);
        string AddTorrent(byte[] torrent, Uri[] uris = null, Options options = null, int position = -1);
        string AddMetalink(byte[] metalink);
        string AddMetalink(byte[] metalink, Options options);
        string AddMetalink(byte[] metalink, Options options, int position);
        string Remove(string gid);
        string ForceRemove(string gid);
        string Pause(string gid);
        string PauseAll();
        string ForcePause(string gid);
        string ForcePauseAll();
        string Unpause(string gid);
        string UnpauseAll();
        StatusResponse TellStatus(string gid);
        StatusResponse TellStatus(string gid, string[] keys);
        UriResponse[] GetUris(string gid);
        FileResponse[] GetFiles(string gid);
        PeerResponse[] GetPeers(string gid);
        ServersResponse[] GetServers(string gid);
        StatusResponse[] TellActive();
        StatusResponse[] TellActive(string[] keys);
        StatusResponse[] TellWaiting(int offset, int num);
        StatusResponse[] TellWaiting(int offset, int num, string[] keys);
        StatusResponse[] TellStopped(int offset, int num);
        StatusResponse[] TellStopped(int offset, int num, string[] keys);
        int ChangePosition(string gid, int pos, string how);
        int[] ChangeUri(string gid, int fileIndex, string[] delUris, string[] addUris);
        int[] ChangeUri(string gid, int fileIndex, string[] delUris, string[] addUris, int position);
        IDictionary<string, string> GetOption(string gid);
        string ChangeOption(string gid, Options options);
        Options GetGlobalOption();
        string ChangeGlobalOption(Options options);
        GlobalStatResponse GetGlobalStat();
        string PurgeDownloadResult();
        string RemoveDownloadResult(string gid);
        VersionResponse GetVersion();
        SessionInfoResponse GetSessionInfo();
        bool Shutdown();
        bool ForceShutdown();
    }
}

