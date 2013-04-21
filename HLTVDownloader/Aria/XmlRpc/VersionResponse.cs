using CookComputing.XmlRpc;

namespace PaganSoft.HLTVDownloader.Aria.XmlRpc
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
