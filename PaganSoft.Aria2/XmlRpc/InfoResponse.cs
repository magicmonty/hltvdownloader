using CookComputing.XmlRpc;

namespace PaganSoft.Aria2.XmlRpc
{
    public struct InfoResponse
    {
        [XmlRpcMember("name")]
        public string
            Name;
    }
    
}
