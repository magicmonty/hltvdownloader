using System.Collections.Generic;

namespace Pagansoft.Aria2.Core
{
    public interface IVersionInfo
    {
        string Version { get; }

        IEnumerable<string> EnabledFeatures { get; }
    }
}
