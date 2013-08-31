using CookComputing.XmlRpc;

namespace Pagansoft.Aria2.XmlRpc
{
    public struct GlobalStatResponse
    {
        [XmlRpcMember("downloadSpeed")]
        public string
            DownloadSpeed;
        [XmlRpcMember("uploadSpeed")]
        public string
            UploadSpeed;
        [XmlRpcMember("numActive")]
        public string
            NumActive;
        [XmlRpcMember("numWaiting")]
        public string
            NumWaiting;
        [XmlRpcMember("numStopped")]
        public string
            NumStopped;
    }
}
