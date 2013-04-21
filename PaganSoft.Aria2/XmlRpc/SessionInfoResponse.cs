using CookComputing.XmlRpc;

namespace PaganSoft.Aria2.XmlRpc
{

    public struct SessionInfoResponse
    {
        [XmlRpcMember("sessionId")]
        public string
            SessionId;
    }
    
}
