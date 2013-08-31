using CookComputing.XmlRpc;

namespace Pagansoft.Aria2.XmlRpc
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
