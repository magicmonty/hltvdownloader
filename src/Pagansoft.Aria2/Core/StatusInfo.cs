using System.Collections.Generic;
using System.Linq;
using Pagansoft.Aria2.XmlRpc;

namespace Pagansoft.Aria2.Core
{
    public struct StatusInfo : IStatusInfo
    {
        public static IStatusInfo From(StatusResponse response)
        {
            return new StatusInfo {
                Gid = response.Gid,
                Status = response.Status,
                TotalLength = long.Parse(response.TotalLength),
                CompletedLength = long.Parse(response.CompletedLength),
                UploadLength = long.Parse(response.UploadLength),
                BitField = response.BitField,
                DownloadSpeed = long.Parse(response.DownloadSpeed),
                UploadSpeed = long.Parse(response.UploadSpeed),
                InfoHash = response.InfoHash,
                NumSeeders = int.Parse(response.NumSeeders),
                PieceLength = long.Parse(response.PieceLength),
                NumPieces = int.Parse(response.NumPieces),
                Connections = int.Parse(response.Connections),
                ErrorCode = response.ErrorCode,
                FollowedBy = response.FollowedBy,
                BelongsTo = response.BelongsTo,
                Dir = response.Dir,
                Files = response.Files.Select(FileInfo.From),
                BitTorrent = BitTorrentInfo.From(response.BitTorrent)
            };
        }

        public string Gid { get; private set; }

        public string Status { get; private set; }

        public long TotalLength { get; private set; }

        public long CompletedLength { get; private set; }

        public long UploadLength { get; private set; }

        public string BitField { get; private set; }

        public long DownloadSpeed { get; private set; }

        public long UploadSpeed { get; private set; }

        public string InfoHash { get; private set; }

        public int NumSeeders { get; private set; }

        public long PieceLength { get; private set; }

        public int NumPieces { get; private set; }

        public int Connections { get; private set; }

        public string ErrorCode { get; private set; }

        public IEnumerable<string> FollowedBy { get; private set; }

        public string BelongsTo { get; private set; }

        public string Dir { get; private set; }

        public IEnumerable<IFileInfo> Files { get; private set; }

        public IBitTorrentInfo BitTorrent { get; private set; }
    }
}
