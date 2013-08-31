using CookComputing.XmlRpc;

namespace Pagansoft.Aria2.XmlRpc
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
