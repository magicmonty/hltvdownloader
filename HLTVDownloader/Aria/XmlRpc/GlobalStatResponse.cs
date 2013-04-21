using CookComputing.XmlRpc;

namespace PaganSoft.HLTVDownloader.Aria.XmlRpc
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
