using System.Collections.Generic;
using Pagansoft.Aria2.Options.Enums;

namespace Pagansoft.Aria2.Options
{
    public struct BitTorrentOptions
    {
        /// <summary>
        /// Enable Local Peer Discovery. If a private flag is set in a torrent, 
        /// aria2 doesn't use this feature for that download even if true is given. 
        /// Default: false
        /// </summary>
        public bool EnableLpd;
        /// <summary>
        /// List of BitTorrent tracker's announce URI to remove. 
        /// You can use special value * which matches all URIs, thus removes 
        /// all announce URIs. When specifying * in shell command-line, 
        /// don't forget to escape or quote it. <seealso cref="Tracker"/> option.
        /// </summary>
        public IEnumerable<string> ExcludeTracker;
        /// <summary>
        /// Specify the external IP address to report to a BitTorrent tracker. 
        /// Although this function is named external, it can accept any kind 
        /// of IP addresses. IPADDRESS must be a numeric IP address.
        /// </summary>
        public string ExternalIp;
        /// <summary>
        /// If true is given, after hash check using <see cref="Options.CheckIntegrity"/> 
        /// option and file is complete, continue to seed file. If you want to check file 
        /// and download it only when it is damaged or incomplete, set this option to 
        /// false. This option has effect only on BitTorrent download. 
        /// Default: true
        /// </summary>
        public string HashCheckSeed;
        /// <summary>
        /// Specify maximum number of files to open in each BitTorrent download. 
        /// Default: 100
        /// </summary>
        public int MaxOpenFiles;
        /// <summary>
        /// Specify the maximum number of peers per torrent. 
        /// 0 means unlimited. 
        /// <seealso cref="RequestPeerSpeedLimit"/> option. 
        /// Default: 55
        /// </summary>
        public int MaxPeers;
        /// <summary>
        /// Download metadata only. 
        /// The file(s) described in metadata will not be downloaded. 
        /// This option has effect only when BitTorrent Magnet URI is used. 
        /// <seealso cref="SaveMetadata"/> option. 
        /// Default: false
        /// </summary>
        public bool MetadataOnly;
        /// <summary>
        /// Set minimum level of encryption method. 
        /// If several encryption methods are provided by a peer, 
        /// aria2 chooses the lowest one which satisfies the given level. 
        /// Default: plain
        /// </summary>
        public BitTorrentCryptoLevelOption MinCryptoLevel;
        /// <summary>
        /// Try to download first and last pieces of each file first. 
        /// This is useful for previewing files. 
        /// The argument can contain 2 keywords: head and tail. 
        /// To include both keywords, they must be separated by comma. 
        /// These keywords can take one parameter, SIZE. 
        /// For example, if head=<SIZE> is specified, pieces in the range of 
        /// first SIZE bytes of each file get higher priority. 
        /// tail=<SIZE> means the range of last SIZE bytes of each file. 
        /// SIZE can include K or M (1K = 1024, 1M = 1024K). 
        /// If SIZE is omitted, SIZE=1M is used.
        /// </summary>
        public string PrioritizePiece;
        /// <summary>
        /// Removes the unselected files when download is completed in BitTorrent. 
        /// To select files, use <see cref="Options.SelectFile"/> option. 
        /// If it is not used, all files are assumed to be selected. 
        /// Please use this option with care because it will actually remove files 
        /// from your disk. 
        /// Default: false
        /// </summary>
        public bool RemoveUnselectedFile;
        /// <summary>
        /// If the whole download speed of every torrent is lower than SPEED, 
        /// aria2 temporarily increases the number of peers to try for more 
        /// download speed. 
        /// Configuring this option with your preferred download speed can increase 
        /// your download speed in some cases. 
        /// You can append K or M (1K = 1024, 1M = 1024K). 
        /// Default: 50K
        /// </summary>
        public long RequestPeerSpeedLimit;
        /// <summary>
        /// If true is given, aria2 doesn't accept and establish connection with legacy 
        /// BitTorrent handshake(19BitTorrent protocol). 
        /// Thus aria2 always uses Obfuscation handshake. 
        /// Default: false
        /// </summary>
        public bool RequireCrypto;
        /// <summary>
        /// Save metadata as ".torrent" file. This option has effect 
        /// only when BitTorrent Magnet URI is used. The filename is hex 
        /// encoded info hash with suffix ".torrent". 
        /// The directory to be saved is the same directory where download 
        /// file is saved. If the same file already exists, metadata is not saved. 
        /// <seealso cref="MetadataOnly"/> option. 
        /// Default: false
        /// </summary>
        public bool SaveMetadata;
        /// <summary>
        /// Seed previously downloaded files without verifying piece hashes. 
        /// Default: false
        /// </summary>
        public bool SeedUnverified;
        /// <summary>
        /// Stop BitTorrent download if download speed is 0 in 
        /// consecutive SEC seconds. 
        /// If 0 is given, this feature is disabled. 
        /// Default: 0
        /// </summary>
        public int StopTimeout;
        /// <summary>
        /// List of additional BitTorrent tracker's announce URI. 
        /// These URIs are not affected by <see cref="ExcludeTracker"/>≈ option because 
        /// they are added after URIs in <see cref="ExcludeTracker"/> option are removed.
        /// </summary>
        public IEnumerable<string> Tracker;
        /// <summary>
        /// Set the connect timeout in seconds to establish connection to tracker. 
        /// After the connection is established, this option makes no effect and 
        /// <see cref="TrackerTimeout"/> option is used instead. 
        /// Default: 60
        /// </summary>
        public int TrackerConnectTimeout;
        /// <summary>
        /// Set the interval in seconds between tracker requests. 
        /// This completely overrides interval value and aria2 just 
        /// uses this value and ignores the min interval and interval 
        /// value in the response of tracker. 
        /// If 0 is set, aria2 determines interval based on the response 
        /// of tracker and the download progress. 
        /// Default: 0
        /// </summary>
        public int TrackerInterval;
        /// <summary>
        /// Set timeout in seconds. 
        /// Default: 60
        /// </summary>
        public int TrackerTimeout;
    }
}
