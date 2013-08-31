using System.Collections.Generic;
using Pagansoft.Aria2.XmlRpc;

namespace Pagansoft.Aria2.Core
{
    public class VersionInfo : IVersionInfo
    {
        public static VersionInfo From(VersionResponse response)
        {
            return new VersionInfo {
                Version = response.Version,
                EnabledFeatures = response.EnabledFeatures
            };
        }

        public string Version { get; private set; }

        public IEnumerable<string> EnabledFeatures { get; private set; }
    }
}
