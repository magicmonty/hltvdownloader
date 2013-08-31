using System;
using System.Collections.Generic;
using Pagansoft.Aria2.Core;
using Pagansoft.Aria2.XmlRpc;

namespace Pagansoft.Aria2.Core
{
    public class BitTorrentInfo : IBitTorrentInfo
    {
        public static BitTorrentInfo From(BitTorrentResponse response)
        {
            return new BitTorrentInfo {
                AnnounceList = response.AnnounceList,
                Comment = response.Comment,
                CreationDate = DateTime.Parse(response.CreationDate),
                Mode = response.Mode,
                Name = response.Info.Name
            };
        }

        public IEnumerable<string> AnnounceList { get; set; }

        public string Comment { get; set; }

        public DateTime CreationDate { get; set; }

        public string Mode { get; set; }

        public string Name { get; set; }
    }
}
