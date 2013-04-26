using System.Collections.Generic;
using PaganSoft.Aria2.Options.Enums;

namespace PaganSoft.Aria2.Options
{
    /// <summary>
    /// The aria2 options
    /// </summary>
    public struct AriaOptions
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

        /// <summary>
        /// Restart download from scratch if the corresponding control 
        /// file doesn't exist. <seealso cref="AutoFileRenaming"/> option. 
        /// Default: false
        /// </summary>
        public bool AllowOverwrite;

        /// <summary>
        /// If false is given, aria2 aborts download when a piece 
        /// length is different from one in a control file. If true is given, 
        /// you can proceed but some download progress will be lost. 
        /// Default: false
        /// </summary>
        public bool AllowPieceLengthChange;

        /// <summary>
        /// Always resume download. If true is given, aria2 always tries to 
        /// resume download and if resume is not possible, aborts download. 
        /// If false is given, when all given URIs do not support resume or 
        /// aria2 encounters N URIs which does not support resume 
        /// (N is the value specified using --max-resume-failure-tries option), 
        /// aria2 downloads file from scratch. 
        /// <see cref="MaxResumeFailureTries"/> option. 
        /// Default: true
        /// </summary>
        public bool AlwaysResume;

        /// <summary>
        /// Enable asynchronous DNS. 
        /// Default: true
        /// </summary>
        public bool AsyncDns;

        /// <summary>
        /// Rename file name if the same file already exists. 
        /// This option works only in HTTP(S)/FTP download. 
        /// The new file name has a dot and a number(1..9999) appended. 
        /// Default: true
        /// </summary>
        public bool AutoFileRenaming;

        /// <summary>
        /// The bit torrent specific options.
        /// </summary>
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
        public CheckSumOption Checksum;

        /// <summary>
        /// Download file only when the local file is older than remote file. 
        /// This function only works with HTTP(S) downloads only. 
        /// It does not work if file size is specified in Metalink. 
        /// It also ignores Content-Disposition header. 
        /// If a control file exists, this option will be ignored. 
        /// This function uses If-Modified-Since header to get only newer file 
        /// conditionally. 
        /// When getting modification time of local file, it uses user supplied 
        /// filename(<see cref="Out"/> option) or filename part in URI if <see cref="Out"/> 
        /// is not specified. To overwrite existing file, <see cref="AllowOverwrite"/> 
        /// is required. 
        /// Default: false
        /// </summary>
        public bool ConditionalGet;

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

        /// <summary>
        /// Enable IPv6 name resolution in asynchronous DNS resolver. 
        /// This option will be ignored when <see cref="AsyncDns"/>=false. 
        /// Default: false
        /// </summary>
        public bool EnableAsyncDns6;

        /// <summary>
        /// Enable HTTP/1.1 persistent connection. 
        /// Default: true
        /// </summary>
        public bool EnableHttpKeepAlive;

        /// <summary>
        /// Enable HTTP/1.1 pipelining. 
        /// Default: false
        /// </summary>
        public bool EnableHttpPipelining;

        /// <summary>
        /// Map files into memory. This option may not work if the file space is 
        /// not pre-allocated. <see cref="FileAllocation"/>.
        /// Default: false
        /// </summary>
        public bool EnableMmap;

        /// <summary>
        /// Enable Peer Exchange extension. If a private flag is set in a torrent, 
        /// this feature is disabled for that download even if true is given. 
        /// Default: true
        /// </summary>
        public bool EnablePeerExchange;

        /// <summary>
        /// Specify file allocation method. 
        /// None doesn't pre-allocate file space. 
        /// Prealloc pre-allocates file space before download begins. 
        /// This may take some time depending on the size of the file. 
        /// If you are using newer file systems such as ext4 (with extents support), 
        /// btrfs, xfs or NTFS(MinGW build only), 
        /// Falloc is your best choice. 
        /// It allocates large(few GiB) files almost instantly. 
        /// Don't use Falloc with legacy file systems such as ext3 and FAT32 
        /// because it takes almost same time as Prealloc and it blocks aria2 
        /// entirely until allocation finishes. 
        /// Falloc may not be available if your system doesn't have posix_fallocate(3) 
        /// function. 
        /// Trunc uses ftruncate(2) system call or platform-specific counterpart to 
        /// truncate a file to a specified length.
        /// Possible Values: none, prealloc, trunc, falloc 
        /// Default: prealloc
        /// </summary>
        public FileAllocationOption FileAllocation;

        /// <summary>
        /// If True or Mem is specified, when a file whose suffix is .meta4 or .metalink 
        /// or content type of application/metalink4+xml or application/metalink+xml 
        /// is downloaded, aria2 parses it as a metalink file and downloads files 
        /// mentioned in it. 
        /// If Mem is specified, a metalink file is not written to  the disk, 
        /// but is just kept in memory. 
        /// If False is specified, the action mentioned above is not taken. 
        /// Default: True
        /// </summary>
        public FollowOption FollowMetalink;

        /// <summary>
        /// if True or Mem is specified, when a file whose suffix is .torrent or 
        /// content type is application/x-bittorrent is downloaded, 
        /// aria2 parses it as a torrent file and downloads files mentioned in it. 
        /// If Mem is specified, a torrent file is not written to the disk, but is 
        /// just kept in memory. 
        /// If False is specified, the action mentioned above is not taken. 
        /// Default: True
        /// </summary>
        public FollowOption FollowTorrent;

        /// <summary>
        /// Save download with <see cref="SaveSession"/> option even if the download 
        /// is completed or removed. 
        /// This may be useful to save BitTorrent seeding which is recognized as 
        /// completed state. 
        /// Default: false
        /// </summary>
        public bool ForceSave;

        /// <summary>
        /// The FTP specific options
        /// </summary>
        public FtpOptions Ftp;

        /// <summary>
        /// If true is given, after hash check using <see cref="CheckIntegrity"/> option, 
        /// abort download whether or not download is complete. 
        /// Default: false
        /// </summary>
        public bool HashCheckOnly;

        /// <summary>
        /// Append HEADER to HTTP request header. 
        /// You can use this to specify more than one header
        /// </summary>
        public IEnumerable<string> Header;

        /// <summary>
        /// The HTTP specific options.
        /// </summary>
        public HttpOptions Http;
        
        /// <summary>
        /// The HTTPS specific options.
        /// </summary>
        public HttpsOptions Https;

        /// <summary>
        /// Set file path for file with index=INDEX. 
        /// You can find the file index using the ShowFiles option. 
        /// PATH is a relative path to the path specified in <see cref="Dir"/> option. 
        /// You can use this option multiple times. 
        /// Using this option, you can specify the output filenames of BitTorrent downloads.
        /// </summary>
        public IEnumerable<IDictionary<int, string>> IndexOut;

        /// <summary>
        /// lose connection if download speed is lower than or 
        /// equal to this value(bytes per sec). 
        /// 0 means aria2 does not have a lowest speed limit. 
        /// You can append K or M (1K = 1024, 1M = 1024K). 
        /// This option does not affect BitTorrent downloads. 
        /// Default: 0
        /// </summary>
        public long LowestSpeedLimit;

        /// <summary>
        /// The maximum number of connections to one server for each download. 
        /// Default: 1
        /// </summary>
        public int MaxConnectionPerServer;

        /// <summary>
        /// Set max download speed per each download in bytes/sec. 
        /// 0 means unrestricted. 
        /// Default: 0
        /// </summary>
        public long MaxDownloadLimit;

        /// <summary>
        /// If aria2 receives "file not found" status from the remote HTTP/FTP servers 
        /// NUM times without getting a single byte, then force the download to fail. 
        /// Specify 0 to disable this option. This options is effective only when 
        /// using HTTP/FTP servers. 
        /// Default: 0
        /// </summary>
        public int MaxFileNotFound;

        /// <summary>
        /// When used with <see cref="AlwaysResume"/>=false, aria2 downloads file 
        /// from scratch when aria2 detects N number of URIs that does not support resume. 
        /// If N is 0, aria2 downloads file from scratch when all given URIs do not 
        /// support resume. 
        /// Default: 0
        /// </summary>
        public string MaxResumeFailureTries;

        /// <summary>
        /// Set number of tries. 
        /// 0 means unlimited. 
        /// <seealso cref="RetryWait" />. 
        /// Default: 5
        /// </summary>
        public int MaxTries;

        /// <summary>
        /// Set max upload speed per each torrent in bytes/sec. 
        /// 0 means unrestricted.
        /// Default: 0
        /// </summary>
        public long MaxUploadLimit;

        /// <summary>
        /// The metalink specific options.
        /// </summary>
        public MetaLinkOptions Metalink;

        /// <summary>
        /// aria2 does not split less than 2 * MinSplitSize byte range. 
        /// For example, let's consider downloading 20MiB file. 
        /// If MinSplitSize is 10M, aria2 can split file into 2 range [0-10MiB) 
        /// and [10MiB-20MiB) and download it using 2 sources(if Split >= 2, of course). 
        /// If MinSplitSize is 15M, since 2*15M > 20MiB, aria2 does not split file and 
        /// download it using 1 source. 
        /// Possible Values: 1M -1024M 
        /// Default: 20M
        /// </summary>
        public long MinSplitSize;

        /// <summary>
        /// No file allocation is made for files whose size is smaller than SIZE.
        /// Default: 5M
        /// </summary>
        public long NoFileAllocationLimit;

        /// <summary>
        /// Disables netrc support. 
        /// netrc support is enabled by default.
        /// </summary>
        public bool NoNetrc;

        /// <summary>
        /// Specify hostnames, domains and network addresses 
        /// with or without CIDR block where proxy should not be used.
        /// </summary>
        public IEnumerable<string> NoProxy;

        /// <summary>
        /// The file name of the downloaded file. 
        /// When ForceSequential option is used, this option is ignored.
        /// </summary>
        public string Out;

        /// <summary>
        /// Enable parameterized URI support. You can specify set of 
        /// parts: http://{sv1,sv2,sv3}/foo.iso. 
        /// Also you can specify numeric sequences with step counter: 
        /// http://host/image[000-100:2].img. 
        /// A step counter can be omitted. 
        /// If all URIs do not point to the same file, such as the second example above, 
        /// -Z option is required. 
        /// Default: false
        /// </summary>
        public bool ParameterizedUri;

        /// <summary>
        /// Pause download after added. 
        /// This option is effective only when RPC is enabled. 
        /// Default: false
        /// </summary>
        public bool Pause;

        /// <summary>
        /// Set a piece length for HTTP/FTP downloads. 
        /// This is the boundary when aria2 splits a file. 
        /// All splits occur at multiple of this length. 
        /// This option will be ignored in BitTorrent downloads. 
        /// It will be also ignored if Metalink file contains piece hashes. 
        /// Default: 1M
        /// </summary>
        public long PieceLength;

        /// <summary>
        /// Set the method to use in proxy request. 
        /// ProxyMethod is either get or tunnel. 
        /// HTTPS downloads always use tunnel regardless of this option. 
        /// Default: get
        /// </summary>
        public ProxyMethodOption ProxyMethod;

        /// <summary>
        /// Validate chunk of data by calculating checksum while 
        /// downloading a file if chunk checksums are provided. 
        /// Default: true
        /// </summary>
        public bool RealtimeChunkChecksum;

        /// <summary>
        /// Set Referer. This affects all URIs. 
        /// If * is given, each request URI is used as a referer. 
        /// This may be useful when used with <see cref="ParameterizedUri"/> option.
        /// </summary>
        public string Referer;

        /// <summary>
        /// Retrieve timestamp of the remote file from the remote HTTP/FTP server 
        /// and if it is available, apply it to the local file. 
        /// Default: false
        /// </summary>
        public bool RemoteTime;

        /// <summary>
        /// Remove control file before download. 
        /// Using with <see cref="AllowOverwrite"/>=true, download always starts 
        /// from scratch. 
        /// This will be useful for users behind proxy server which disables resume.
        /// </summary>
        public bool RemoveControlFile;

        /// <summary>
        /// Set the seconds to wait between retries. 
        /// With RetryWait > 0, aria2 will retry download when the 
        /// HTTP server returns 503 response. 
        /// Default: 0
        /// </summary>
        public int RetryWait;

        /// <summary>
        /// Reuse already used URIs if no unused URIs are left. 
        /// Default: true
        /// </summary>
        public bool ReuseUri;

        /// <summary>
        /// Save the uploaded torrent or metalink metadata in the directory 
        /// specified by <see cref="Dir"/> option. The filename consists of 
        /// SHA-1 hash hex string of metadata plus extension. 
        /// For torrent, the extension is '.torrent'. 
        /// For metalink, it is '.meta4'. 
        /// If false is given to this option, the downloads added by AddTorrent() or 
        /// AddMetalink() will not be saved by <see cref="SaveSession"/> option. 
        /// Default: false
        /// </summary>
        public bool RpcSaveUploadMetadata;

        /// <summary>
        /// Specify share ratio. Seed completed torrents until share ratio reaches RATIO. 
        /// You are strongly encouraged to specify equals or more than 1.0 here. 
        /// Specify 0.0 if you intend to do seeding regardless of share ratio. 
        /// If <see cref="SeedTime"> option is specified along with this option, 
        /// seeding ends when at least one of the conditions is satisfied. 
        /// Default: 1.0
        /// </summary>
        public double SeedRatio;

        /// <summary>
        /// Specify seeding time in minutes. <seealso cref="SeedRatio"/> option.
        /// </summary>
        public int SeedTime;

        /// <summary>
        /// Set file to download by specifying its index. 
        /// You can find the file index using the ShowFiles option. 
        /// Multiple indexes can be specified by using ",", for example: 3,6. 
        /// You can also use - to specify a range: 1-5. , and - can be used together: 1-5,8,9. 
        /// When used with the -M option, index may vary depending on the query 
        /// (see --metalink-* options).
        /// </summary>
        public string SelectFile;

        /// <summary>
        /// Download a file using N connections. If more than N URIs are given, 
        /// first N URIs are used and remaining URIs are used for backup. 
        /// If less than N URIs are given, those URIs are used more than once so that 
        /// N connections total are made simultaneously. 
        /// The number of connections to the same host is restricted by MaxConnectionPerServer option. 
        /// <seealso cref="MinSplitSize"/> option. 
        /// Default: 5
        /// </summary>
        public int Split;

        /// <summary>
        /// Specify piece selection algorithm used in HTTP/FTP download. 
        /// Piece means fixed length segment which is downloaded in parallel in 
        /// segmented download. 
        /// If default is given, aria2 selects piece so that it reduces the number of 
        /// establishing connection. This is reasonable default behaviour because 
        /// establishing connection is an expensive operation. 
        /// If inorder is given, aria2 selects piece which has minimum index. 
        /// Index=0 means first of the file. 
        /// This will be useful to view movie while downloading it. 
        /// <see cref="EnableHttpPipelining"/> option may be useful to reduce reconnection 
        /// overhead. 
        /// Please note that aria2 honors <see cref="MinSplitSize"/> option, so it will be 
        /// necessary to specify a reasonable value to <see cref="MinSplitSize"/> option. 
        /// If geom is given, at the beginning aria2 selects piece which has 
        /// minimum index like inorder, but it exponentially increasingly keeps space 
        /// from previously selected piece. 
        /// This will reduce the number of establishing connection and at the same time 
        /// it will download the beginning part of the file first. This will be useful 
        /// to view movie while downloading it. 
        /// Default: default
        /// </summary>
        public string StreamPieceSelector;

        /// <summary>
        /// Set timeout in seconds. 
        /// Default: 60
        /// </summary>
        public int Timeout;

        /// <summary>
        /// Specify URI selection algorithm. 
        /// The possible values are inorder, feedback and adaptive. 
        /// If inorder is given, URI is tried in the order appeared in the URI list. 
        /// If feedback is given, aria2 uses download speed observed in the previous 
        /// downloads and choose fastest server in the URI list. 
        /// This also effectively skips dead mirrors. 
        /// If adaptive is given, selects one of the best mirrors for the first and 
        /// reserved connections. For supplementary ones, it returns mirrors which 
        /// has not been tested yet, and if each of them has already been tested, 
        /// returns mirrors which has to be tested again. 
        /// Otherwise, it doesn't select anymore mirrors. Like feedback, it uses a 
        /// performance profile of servers. 
        /// Default: feedback
        /// </summary>
        public URISelectorOption UriSelector;

        /// <summary>
        /// Use HEAD method for the first request to the HTTP server. 
        /// Default: false
        /// </summary>
        public bool UseHead;

        /// <summary>
        /// Set user agent for HTTP(S) downloads. 
        /// Default: aria2/$VERSION, $VERSION is replaced by package version.
        /// </summary>
        public string UserAgent;
    }
}
