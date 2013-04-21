using CookComputing.XmlRpc;

namespace PaganSoft.HLTVDownloader.Aria.XmlRpc
{

    public struct ServerResponse
    {
        [XmlRpcMember("uri")]
        public string
            Uri;
        [XmlRpcMember("currentUri")]
        public string
            CurrentUri;
        [XmlRpcMember("downloadSpeed")]
        public string
            DownloadSpeed;
    }
    
}
