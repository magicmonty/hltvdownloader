using CookComputing.XmlRpc;

namespace PaganSoft.HLTVDownloader.Aria.XmlRpc
{

    public struct ServersResponse
    {
        [XmlRpcMember("index")]
        public string
            Index;
        [XmlRpcMember("servers")]
        public ServerResponse[]
            Servers;
    }
    
}
