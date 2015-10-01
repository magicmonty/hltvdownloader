using XmlRpcLight.DataTypes;
using XmlRpcLight.Enums;
using XmlRpcLight.Attributes;

namespace Pagansoft.Aria2.XmlRpc
{
    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public struct BitTorrentResponse
    {
        [XmlRpcMember("announceList")]
        public string[]
            AnnounceList;
        [XmlRpcMember("comment")]
        public string
            Comment;
        [XmlRpcMember("creationDate")]
        public string
            CreationDate;
        [XmlRpcMember("mode")]
        public string
            Mode;
        [XmlRpcMember("info")]
        public InfoResponse
            Info;
    }
}
