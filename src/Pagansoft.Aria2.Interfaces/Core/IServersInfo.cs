using System.Collections.Generic;

namespace Pagansoft.Aria2.Core
{
    public interface IServersInfo
    {
        int Index { get; }

        IEnumerable<IServerInfo> Servers { get; }
    }
}
