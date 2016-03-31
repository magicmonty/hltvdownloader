using System;

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

        public string Gid { get; }

        public string ListId { get; }

        public string LinkId { get; }

        public string Url { get; }

        public override bool Equals(object obj)
        {
            var other = obj as LinkIdPersistenceModel;
            if (other == (object)null)
                return false;

            return string.Equals(other.Gid, Gid, StringComparison.OrdinalIgnoreCase)
                && string.Equals(other.LinkId, LinkId, StringComparison.OrdinalIgnoreCase)
                && string.Equals(other.ListId, ListId, StringComparison.OrdinalIgnoreCase)
                && string.Equals(other.Url, Url, StringComparison.OrdinalIgnoreCase);
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

