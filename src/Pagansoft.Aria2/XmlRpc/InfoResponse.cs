using XmlRpcLight.Attributes;

namespace Pagansoft.Aria2.XmlRpc
{
    public struct InfoResponse
    {
        [XmlRpcMember("name")]
        public string
            Name;
    }
}
