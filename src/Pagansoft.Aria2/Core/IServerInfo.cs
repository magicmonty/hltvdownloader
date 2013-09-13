using System;

namespace Pagansoft.Aria2.Core
{
    public interface IServerInfo
    {
        Uri Uri { get; }

        Uri CurrentUri { get; }

        long DownloadSpeed { get; }
    }
}
