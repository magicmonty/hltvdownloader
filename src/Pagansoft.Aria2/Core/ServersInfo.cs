using System.Collections.Generic;
using Pagansoft.Aria2.XmlRpc;
using System.Linq;

namespace Pagansoft.Aria2.Core
{
    public class ServersInfo : IServersInfo
    {
        public static ServersInfo From(ServersResponse response)
        {
            return new ServersInfo {
                Index = int.Parse(response.Index),
                Servers = response.Servers.Select(ServerInfo.From)
            };
        }

        public int Index { get; private set; }

        public IEnumerable<IServerInfo> Servers { get; private set; }
    }
}
