using CookComputing.XmlRpc;

namespace Pagansoft.Aria2.XmlRpc
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
