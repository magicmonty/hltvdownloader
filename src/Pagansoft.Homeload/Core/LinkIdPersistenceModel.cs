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
    }
}

