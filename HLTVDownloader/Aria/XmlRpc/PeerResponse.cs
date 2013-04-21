using CookComputing.XmlRpc;

namespace PaganSoft.HLTVDownloader.Aria.XmlRpc
{

    public struct PeerResponse
    {
        [XmlRpcMember("peerId")]
        public string
            PeerId;
        [XmlRpcMember("ip")]
        public string
            Ip;
        [XmlRpcMember("port")]
        public string
            Port;
        [XmlRpcMember("bitfield")]
        public string
            BitField;
        [XmlRpcMember("amChoking")]
        public string
            AmChoking;
        [XmlRpcMember("peerChoking")]
        public string
            PeerChoking;
        [XmlRpcMember("downloadSpeed")]
        public string
            DownloadSpeed;
        [XmlRpcMember("uploadSpeed")]
        public string
            UploadSpeed;
        [XmlRpcMember("seeder")]
        public string
            Seeder;
    }
    
}
