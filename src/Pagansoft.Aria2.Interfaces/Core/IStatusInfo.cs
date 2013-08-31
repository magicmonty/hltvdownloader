using System.Collections.Generic;

namespace Pagansoft.Aria2.Core
{
    public interface IStatusInfo
    {
        string Gid { get; }

        string Status { get; }

        long TotalLength { get; }

        long CompletedLength { get; }

        long UploadLength { get; }

        string BitField { get; }

        long DownloadSpeed { get; }

        long UploadSpeed { get; }

        string InfoHash { get; }

        int NumSeeders { get; }

        long PieceLength { get; }

        int NumPieces { get; }

        int Connections { get; }

        string ErrorCode { get; }

        IEnumerable<string> FollewedBy { get; }

        string BelongsTo { get; }

        string Dir { get; }

        IEnumerable<IFileInfo> Files { get; }

        IBitTorrentInfo BitTorrent { get; }
    }
}
