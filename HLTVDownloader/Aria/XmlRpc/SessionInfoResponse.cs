using CookComputing.XmlRpc;

namespace PaganSoft.HLTVDownloader.Aria.XmlRpc
{

    public struct SessionInfoResponse
    {
        [XmlRpcMember("sessionId")]
        public string
            SessionId;
    }
    
}
