using System;
using System.Collections.Generic;

namespace Pagansoft.Aria2.Core
{
    public interface IBitTorrentInfo
    {
        IEnumerable<string> AnnounceList { get; }

        string Comment { get; }

        DateTime CreationDate { get; }

        string Mode { get; }

        string Name { get; }
    }
}

