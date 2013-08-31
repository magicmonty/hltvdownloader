using CookComputing.XmlRpc;

namespace Pagansoft.Aria2.XmlRpc
{
    public struct VersionResponse
    {
        [XmlRpcMember("version")]
        public string
            Version;
        [XmlRpcMember("enabledFeatures")]
        public string[]
            EnabledFeatures;
    }
}
