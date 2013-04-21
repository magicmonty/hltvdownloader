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

    public struct CheckSum
    {
        string Type;
        string Checksum;
    }

    public struct Options
    {
        /// <summary>
        /// Use this proxy server for all protocols. 
        /// To erase previously defined proxy, use "". 
        /// You can override this setting and specify a proxy server for a 
        /// particular protocol using Http.Proxy, Https.Proxy and Ftp.Proxy options. 
        /// This affects all URIs. The format of Proxy is 
        /// [http://][USER:PASSWORD@]HOST[:PORT]. See also ENVIRONMENT section.
        /// </summary>
        public string AllProxy;

        /// <summary>
        /// Set password for AllProxy option
        /// </summary>
        public string AllProxyPasswd;

        /// <summary>
        /// Set user for AllProxy option.
        /// </summary>
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

        /// <summary>
        /// Set checksum. TYPE is hash type. The supported hash type is 
        /// listed in Hash Algorithms in aria2c -v. DIGEST is hex digest. 
        /// For example, setting sha-1 digest looks like this: 
        /// sha-1=0192ba11326fe2298c8cb4de616f4d4140213838 
        /// This option applies only to HTTP(S)/FTP downloads.
        /// </summary>
        public CheckSum Checksum;

        public string ConditionalGet;

        /// <summary>
        /// Set the connect timeout in seconds to establish connection 
        /// to HTTP/FTP/proxy server. After the connection is established, 
        /// this option makes no effect and Timeout option is used instead. 
        /// Default: 60
        /// </summary>
        public int ConnectTimeout;

        /// <summary>
        /// Continue downloading a partially downloaded file. 
        /// Use this option to resume a download started by a web browser or 
        /// another program which downloads files sequentially from the beginning. 
        /// Currently this option is only applicable to HTTP(S)/FTP downloads.
        /// </summary>
        public bool Continue;

        /// <summary>
        /// The directory to store the downloaded file
        /// </summary>
        public string Dir;

        /// <summary>
        /// If true is given, aria2 just checks whether the 
        /// remote file is available and doesn't download data. 
        /// This option has effect on HTTP/FTP download. 
        /// BitTorrent downloads are canceled if true is specified. 
        /// Default: false
        /// </summary>
        public bool DryRun;
        public bool EnableAsyncDns6;
        public bool EnableHttpKeepAlive;
        public bool EnableHttpPipelining;
        public bool EnableMmap;
        public bool EnablePeerExchange;
        public string FileAllocation;
        public bool FollowMetalink;
        public bool FollowTorrent;
        public bool ForceSave;
        public FtpOptions Ftp;
        public string HashCheckOnly;
        public string Header;
        public HttpOptions Http;
        public HttpsOptions Https;
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
