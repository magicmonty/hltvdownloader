using CookComputing.XmlRpc;

namespace PaganSoft.HLTVDownloader.Aria.XmlRpc
{
    public struct InfoResponse
    {
        [XmlRpcMember("name")]
        public string
            Name;
    }
    
}
