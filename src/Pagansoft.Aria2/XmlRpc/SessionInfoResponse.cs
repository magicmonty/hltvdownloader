using CookComputing.XmlRpc;

namespace Pagansoft.Aria2.XmlRpc
{
    public struct SessionInfoResponse
    {
        [XmlRpcMember("sessionId")]
        public string
            SessionId;
    }
}
