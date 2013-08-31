using System;
using System.Collections.Generic;
using Pagansoft.Aria2.Core;

namespace Pagansoft.Aria2.Core
{
    public class BitTorrentInfo : IBitTorrentInfo
    {
        public IEnumerable<string> AnnounceList { get; set; }

        public string Comment { get; set; }

        public DateTime CreationDate { get; set; }

        public string Mode { get; set; }

        public string Name { get; set; }
    }
}
