using CookComputing.XmlRpc;

namespace PaganSoft.Aria2.XmlRpc
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
