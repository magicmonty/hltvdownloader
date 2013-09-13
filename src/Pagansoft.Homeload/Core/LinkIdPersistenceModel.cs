using System.Collections.Generic;

namespace Pagansoft.Homeload.Core
{
    public class LinkIdPersistenceModel
    {
        public LinkIdPersistenceModel(string listId, string linkId, string url, string gid)
        {
            Gid = gid;
            ListId = listId;
            LinkId = linkId;
            Url = url;
        }

        public string Gid { get; private set; }

        public string ListId { get; private set; }

        public string LinkId { get; private set; }

        public string Url { get; private set; }

        public override bool Equals(object obj)
        {
            var other = obj as LinkIdPersistenceModel;
            if (other == (object)null)
                return false;

            return other.Gid == Gid
                && other.LinkId == LinkId
                && other.ListId == ListId
                && other.Url == Url;
        }

        public override int GetHashCode()
        {
            return Gid.GetHashCode()
                ^ ListId.GetHashCode()
                ^ LinkId.GetHashCode()
                ^ Url.GetHashCode();
        }
    }
}

