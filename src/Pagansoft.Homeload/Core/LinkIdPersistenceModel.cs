using Pagansoft.Aria2.Core;
using System.Configuration;

namespace Pagansoft.Homeload.Core
{
    public struct LinkIdPersistenceModel
    {
        public LinkIdPersistenceModel(string listId, string linkId, string url, string gid)
        {
            _gid = gid;
            _listId = listId;
            _linkId = linkId;
            _url = url;
        }

        public GID Gid { get { return _gid; } }

        private GID _gid;

        public string ListId { get { return _listId; } }

        private string _listId;

        public string LinkId { get { return _linkId; } }

        private string _linkId;

        public string Url { get { return _url; } }

        private string _url;
    }
}

