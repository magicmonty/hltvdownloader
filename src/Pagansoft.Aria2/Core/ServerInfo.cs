using System;
using Pagansoft.Aria2.XmlRpc;

namespace Pagansoft.Aria2.Core
{
    public class ServerInfo : IServerInfo
    {
        public static ServerInfo From(ServerResponse response)
        {
            return new ServerInfo {
                Uri = new Uri(response.Uri),
                CurrentUri = new Uri(response.CurrentUri),
                DownloadSpeed = long.Parse(response.DownloadSpeed)
            };
        }

        public Uri Uri { get; private set; }

        public Uri CurrentUri { get; private set; }

        public long DownloadSpeed { get; private set; }
    }
}
