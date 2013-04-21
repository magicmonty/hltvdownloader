using CookComputing.XmlRpc;

namespace PaganSoft.Aria2.XmlRpc
{
    public struct UriResponse
    {
        [XmlRpcMember("uri")]
        public string
            Uri;
        [XmlRpcMember("status")]
        public string
            Status;
    }
    
}
