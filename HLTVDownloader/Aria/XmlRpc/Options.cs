using CookComputing.XmlRpc;

namespace PaganSoft.HLTVDownloader.Aria.XmlRpc
{

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct Options
    {
        [XmlRpcMember("all-proxy")]
        public string
            AllProxy;
        [XmlRpcMember("all-proxy-passwd")]
        public string
            AllProxyPasswd;
        [XmlRpcMember("all-proxy-user")]
        public string
            AllProxyUser;
        [XmlRpcMember("allow-overwrite")]
        public string
            AllowOverwrite;
        [XmlRpcMember("allow-piece-length-change")]
        public string
            AllowPieceLengthChange;
        [XmlRpcMember("always-resume")]
        public string
            AlwaysResume;
        [XmlRpcMember("async-dns")]
        public string
            AsyncDns;
        [XmlRpcMember("auto-file-renaming")]
        public string
            AutoFileRenaming;
        [XmlRpcMember("bt-enable-lpd")]
        public string
            BtEnableLpd;
        [XmlRpcMember("bt-exclude-tracker")]
        public string
            BtExcludeTracker;
        [XmlRpcMember("bt-external-ip")]
        public string
            BtExternalIp;
        [XmlRpcMember("bt-hash-check-seed")]
        public string
            BtHashCheckSeed;
        [XmlRpcMember("bt-max-open-files")]
        public string
            BtMaxOpenFiles;
        [XmlRpcMember("bt-max-peers")]
        public string
            BtMaxPeers;
        [XmlRpcMember("bt-metadata-only")]
        public string
            BtMetadataOnly;
        [XmlRpcMember("bt-min-crypto-level")]
        public string
            BtMinCryptoLevel;
        [XmlRpcMember("bt-prioritize-piece")]
        public string
            BtPrioritizePiece;
        [XmlRpcMember("bt-remove-unselected-file")]
        public string
            BtRemoveUnselectedFile;
        [XmlRpcMember("bt-request-peer-speed-limit")]
        public string
            BtRequestPeerSpeedLimit;
        [XmlRpcMember("bt-require-crypto")]
        public string
            BtRequireCrypto;
        [XmlRpcMember("bt-save-metadata")]
        public string
            BtSaveMetadata;
        [XmlRpcMember("bt-seed-unverified")]
        public string
            BtSeedUnverified;
        [XmlRpcMember("bt-stop-timeout")]
        public string
            BtStopTimeout;
        [XmlRpcMember("bt-tracker")]
        public string
            BtTracker;
        [XmlRpcMember("bt-tracker-connect-timeout")]
        public string
            BtTrackerConnectTimeout;
        [XmlRpcMember("bt-tracker-interval")]
        public string
            BtTrackerInterval;
        [XmlRpcMember("bt-tracker-timeout")]
        public string
            BtTrackerTimeout;
        [XmlRpcMember("check-integrity")]
        public string
            CheckIntegrity;
        [XmlRpcMember("checksum")]
        public string
            Checksum;
        [XmlRpcMember("conditional-get")]
        public string
            ConditionalGet;
        [XmlRpcMember("connect-timeout")]
        public string
            ConnectTimeout;
        [XmlRpcMember("continue")]
        public string
            Continue;
        [XmlRpcMember("dir")]
        public string
            Dir;
        [XmlRpcMember("dry-run")]
        public string
            DryRun;
        [XmlRpcMember("enable-async-dns6")]
        public string
            EnableAsyncDns6;
        [XmlRpcMember("enable-http-keep-alive")]
        public string
            EnableHttpKeepAlive;
        [XmlRpcMember("enable-http-pipelining")]
        public string
            EnableHttpPipelining;
        [XmlRpcMember("enable-mmap")]
        public string
            EnableMmap;
        [XmlRpcMember("enable-peer-exchange")]
        public string
            EnablePeerExchange;
        [XmlRpcMember("file-allocation")]
        public string
            FileAllocation;
        [XmlRpcMember("follow-metalink")]
        public string
            FollowMetalink;
        [XmlRpcMember("follow-torrent")]
        public string
            FollowTorrent;
        [XmlRpcMember("force-save")]
        public string
            ForceSave;
        [XmlRpcMember("ftp-passwd")]
        public string
            FtpPasswd;
        [XmlRpcMember("ftp-pasv")]
        public string
            FtpPasv;
        [XmlRpcMember("ftp-proxy")]
        public string
            FtpProxy;
        [XmlRpcMember("ftp-proxy-passwd")]
        public string
            FtpProxyPasswd;
        [XmlRpcMember("ftp-proxy-user")]
        public string
            FtpProxyUser;
        [XmlRpcMember("ftp-reuse-connection")]
        public string
            FtpReuseConnection;
        [XmlRpcMember("ftp-type")]
        public string
            FtpType;
        [XmlRpcMember("ftp-user")]
        public string
            FtpUser;
        [XmlRpcMember("hash-check-only")]
        public string
            HashCheckOnly;
        [XmlRpcMember("header")]
        public string
            Header;
        [XmlRpcMember("http-accept-gzip")]
        public string
            HttpAcceptGzip;
        [XmlRpcMember("http-auth-challenge")]
        public string
            HttpAuthChallenge;
        [XmlRpcMember("http-no-cache")]
        public string
            HttpNoCache;
        [XmlRpcMember("http-passwd")]
        public string
            HttpPasswd;
        [XmlRpcMember("http-proxy")]
        public string
            HttpProxy;
        [XmlRpcMember("http-proxy-passwd")]
        public string
            HttpProxyPasswd;
        [XmlRpcMember("http-proxy-user")]
        public string
            HttpProxyUser;
        [XmlRpcMember("http-user")]
        public string
            HttpUser;
        [XmlRpcMember("https-proxy")]
        public string
            HttpsProxy;
        [XmlRpcMember("https-proxy-passwd")]
        public string
            HttpsProxyPasswd;
        [XmlRpcMember("https-proxy-user")]
        public string
            HttpsProxyUser;
        [XmlRpcMember("index-out")]
        public string
            IndexOut;
        [XmlRpcMember("lowest-speed-limit")]
        public string
            LowestSpeedLimit;
        [XmlRpcMember("max-connection-per-server")]
        public string
            MaxConnectionPerServer;
        [XmlRpcMember("max-download-limit")]
        public string
            MaxDownloadLimit;
        [XmlRpcMember("max-file-not-found")]
        public string
            MaxFileNotFound;
        [XmlRpcMember("max-resume-failure-tries")]
        public string
            MaxResumeFailureTries;
        [XmlRpcMember("max-tries")]
        public string
            MaxTries;
        [XmlRpcMember("max-upload-limit")]
        public string
            MaxUploadLimit;
        [XmlRpcMember("metalink-base-uri")]
        public string
            MetalinkBaseUri;
        [XmlRpcMember("metalink-enable-unique-protocol")]
        public string
            MetalinkEnableUniqueProtocol;
        [XmlRpcMember("metalink-language")]
        public string
            MetalinkLanguage;
        [XmlRpcMember("metalink-location")]
        public string
            MetalinkLocation;
        [XmlRpcMember("metalink-os")]
        public string
            MetalinkOs;
        [XmlRpcMember("metalink-preferred-protocol")]
        public string
            MetalinkPreferredProtocol;
        [XmlRpcMember("metalink-version")]
        public string
            MetalinkVersion;
        [XmlRpcMember("min-split-size")]
        public string
            MinSplitSize;
        [XmlRpcMember("no-file-allocation-limit")]
        public string
            NoFileAllocationLimit;
        [XmlRpcMember("no-netrc")]
        public string
            NoNetrc;
        [XmlRpcMember("no-proxy")]
        public string
            NoProxy;
        [XmlRpcMember("out")]
        public string
            Out;
        [XmlRpcMember("parameterized-uri")]
        public string
            ParameterizedUri;
        [XmlRpcMember("pause")]
        public string
            Pause;
        [XmlRpcMember("piece-length")]
        public string
            PieceLength;
        [XmlRpcMember("proxy-method")]
        public string
            ProxyMethod;
        [XmlRpcMember("realtime-chunk-checksum")]
        public string
            RealtimeChunkChecksum;
        [XmlRpcMember("referer")]
        public string
            Referer;
        [XmlRpcMember("remote-time")]
        public string
            RemoteTime;
        [XmlRpcMember("remove-control-file")]
        public string
            RemoveControlFile;
        [XmlRpcMember("retry-wait")]
        public string
            RetryWait;
        [XmlRpcMember("reuse-uri")]
        public string
            ReuseUri;
        [XmlRpcMember("rpc-save-upload-metadata")]
        public string
            RpcSaveUploadMetadata;
        [XmlRpcMember("seed-ratio")]
        public string
            SeedRatio;
        [XmlRpcMember("seed-time")]
        public string
            SeedTime;
        [XmlRpcMember("select-file")]
        public string
            SelectFile;
        [XmlRpcMember("split")]
        public string
            Split;
        [XmlRpcMember("stream-piece-selector")]
        public string
            StreamPieceSelector;
        [XmlRpcMember("timeout")]
        public string
            Timeout;
        [XmlRpcMember("uri-selector")]
        public string
            UriSelector;
        [XmlRpcMember("use-head")]
        public string
            UseHead;
        [XmlRpcMember("user-agent")]
        public string
            UserAgent;
    }
    
}
