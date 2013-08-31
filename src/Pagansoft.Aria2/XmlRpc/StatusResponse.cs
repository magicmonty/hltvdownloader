using CookComputing.XmlRpc;

namespace Pagansoft.Aria2.XmlRpc
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct StatusResponse
    {
        [XmlRpcMember("gid")]
        public string
            Gid;
        [XmlRpcMember("status")]
        public string
            Status;
        [XmlRpcMember("totalLength")]
        public string
            TotalLength;
        [XmlRpcMember("completedLength")]
        public string
            CompletedLength;
        [XmlRpcMember("uploadLength")]
        public string
            UploadLength;
        [XmlRpcMember("bitfield")]
        public string
            BitField;
        [XmlRpcMember("downloadSpeed")]
        public string
            DownloadSpeed;
        [XmlRpcMember("uploadSpeed")]
        public string
            UploadSpeed;
        [XmlRpcMember("infoHash")]
        public string
            InfoHash;
        [XmlRpcMember("numSeeders")]
        public string
            NumSeeders;
        [XmlRpcMember("pieceLength")]
        public string
            PieceLength;
        [XmlRpcMember("numPieces")]
        public string
            NumPieces;
        [XmlRpcMember("connections")]
        public string
            Connections;
        [XmlRpcMember("errorCode")]
        public string
            ErrorCode;
        [XmlRpcMember("followedBy")]
        public string[]
            FollowedBy;
        [XmlRpcMember("belongsTo")]
        public string
            BelongsTo;
        [XmlRpcMember("dir")]
        public string
            Dir;
        [XmlRpcMember("files")]
        public FileResponse[]
            Files;
        [XmlRpcMember("bittorrent")]
        public BitTorrentResponse
            BitTorrent;
    }
}
