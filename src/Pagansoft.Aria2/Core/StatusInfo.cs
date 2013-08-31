using System.Collections.Generic;

namespace Pagansoft.Aria2.Core
{
    public struct StatusInfo
    {
        public string Gid;
        public string Status;
        public long TotalLength;
        public long CompletedLength;
        public long UploadLength;
        public string BitField;
        public long DownloadSpeed;
        public long UploadSpeed;
        public string InfoHash;
        public int NumSeeders;
        public long PieceLength;
        public int NumPieces;
        public int Connections;
        public string ErrorCode;
        public IEnumerable<string> FollowedBy;
        public string BelongsTo;
        public string Dir;
        public IEnumerable<FileInfo> Files;
        public BitTorrentInfo BitTorrent;
    }
}
