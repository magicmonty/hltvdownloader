namespace PaganSoft.Aria2
{
    public struct BitTorrentOptions
    {
        public string EnableLpd;
        public string ExcludeTracker;
        public string ExternalIp;
        public string HashCheckSeed;
        public string MaxOpenFiles;
        public string MaxPeers;
        public string MetadataOnly;
        public string MinCryptoLevel;
        public string PrioritizePiece;
        public string RemoveUnselectedFile;
        public string RequestPeerSpeedLimit;
        public string RequireCrypto;
        public string SaveMetadata;
        public string SeedUnverified;
        public string StopTimeout;
        public string Tracker;
        public string TrackerConnectTimeout;
        public string TrackerInterval;
        public string TrackerTimeout;
    }

    public struct FtpOptions
    {
        public string Passwd;
        public bool Pasv;
        public string Proxy;
        public string ProxyPasswd;
        public string ProxyUser;
        public bool ReuseConnection;
        public string Type;
        public string User;
    }

    public struct HttpOptions
    {
        public bool AcceptGzip;
        public string AuthChallenge;
        public bool NoCache;
        public string Passwd;
        public string Proxy;
        public string ProxyPasswd;
        public string ProxyUser;
        public string User;
    }

    public struct HttpsOptions
    {
        public string Proxy;
        public string ProxyPasswd;
        public string ProxyUser;
    }

    public struct MetaLinkOptions
    {
        public string BaseUri;
        public bool EnableUniqueProtocol;
        public string Language;
        public string Location;
        public string Os;
        public string PreferredProtocol;
        public string Version;
    }

    public struct Options
    {
        public string AllProxy;
        public string ProxyPasswd;
        public string AllProxyUser;
        public bool AllowOverwrite;
        public bool AllowPieceLengthChange;
        public bool AlwaysResume;
        public string AsyncDns;
        public string AutoFileRenaming;
        public BitTorrentOptions BitTorrent;

        /// <summary>
        /// Check file integrity by validating piece hashes 
        /// or a hash of entire file. This option has effect only in 
        /// BitTorrent, Metalink downloads with checksums or 
        /// HTTP(S)/FTP downloads with --checksum option. If piece hashes 
        /// are provided, this option can detect damaged portions of a file and 
        /// re-download them. If a hash of entire file is provided, hash check 
        /// is only done when file has been already download. This is determined 
        /// by file length. If hash check fails, file is re-downloaded from scratch. 
        /// If both piece hashes and a hash of entire file are provided, 
        /// only piece hashes are used. Default: false
        /// </summary>
        public bool CheckIntegrity;

        public string Checksum;

        public string ConditionalGet;

        public string ConnectTimeout;

        /// <summary>
        /// Continue downloading a partially downloaded file. 
        /// Use this option to resume a download started by a web browser or 
        /// another program which downloads files sequentially from the beginning. 
        /// Currently this option is only applicable to HTTP(S)/FTP downloads.
        /// </summary>
        public bool Continue;

        /// <summary>The directory to store the downloaded file</summary>
        public string Dir;
        public string DryRun;
        public bool EnableAsyncDns6;
        public bool EnableHttpKeepAlive;
        public bool EnableHttpPipelining;
        public bool EnableMmap;
        public bool EnablePeerExchange;
        public string FileAllocation;
        public bool FollowMetalink;
        public bool FollowTorrent;
        public bool ForceSave;
        public FtpOptions FTP;
        public string HashCheckOnly;
        public string Header;
        public HttpOptions HTTP;
        public HttpsOptions HTTPS;
        public string IndexOut;
        public string LowestSpeedLimit;
        public string MaxConnectionPerServer;
        public string MaxDownloadLimit;
        public string MaxFileNotFound;
        public string MaxResumeFailureTries;
        public string MaxTries;
        public string MaxUploadLimit;
        public MetaLinkOptions Metalink;
        public string MinSplitSize;
        public string NoFileAllocationLimit;
        public string NoNetrc;
        public string NoProxy;
        public string Out;
        public string ParameterizedUri;
        public string Pause;
        public string PieceLength;
        public string ProxyMethod;
        public string RealtimeChunkChecksum;
        public string Referer;
        public string RemoteTime;
        public string RemoveControlFile;
        public string RetryWait;
        public string ReuseUri;
        public string RpcSaveUploadMetadata;
        public string SeedRatio;
        public string SeedTime;
        public string SelectFile;
        public string Split;
        public string StreamPieceSelector;
        public string Timeout;
        public string UriSelector;
        public string UseHead;
        public string UserAgent;
    }
    
}
