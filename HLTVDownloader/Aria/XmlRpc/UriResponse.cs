using CookComputing.XmlRpc;

namespace PaganSoft.HLTVDownloader.Aria.XmlRpc
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
