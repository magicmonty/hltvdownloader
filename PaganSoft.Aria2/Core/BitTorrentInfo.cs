using System.Collections.Generic;
using System;

namespace PaganSoft.Aria2.Core
{
    public struct BitTorrentInfo
    {
        public IEnumerable<string> AnnounceList;
        public string Comment;
        public DateTime CreationDate;
        public string Mode;
        public string Name;
    }
    
}
