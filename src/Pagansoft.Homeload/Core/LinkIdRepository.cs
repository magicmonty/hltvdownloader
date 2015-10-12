using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(ILinkIdRepository))]
    public class LinkIdRepository : ILinkIdRepository
    {
        private readonly IStorage _storage;
        private readonly object _lock;

        [ImportingConstructor]
        public LinkIdRepository(IStorage storage)
        {
            _storage = storage;
            _lock = new object();
        }

        public int LinkCount { get { return Load().Count(); } }

        IEnumerable<LinkIdPersistenceModel> Load()
        {
            lock (_lock)
            {
                return _storage.LoadLinks() ?? Enumerable.Empty<LinkIdPersistenceModel>();
            }
        }

        void Save(IEnumerable<LinkIdPersistenceModel> list)
        {
            lock (_lock)
            {
                _storage.SaveLinks(list);
            }
        }

        public string GetListIdByGid(string gid)
        {
            var item = Load().FirstOrDefault(e => e.Gid == gid);

            if (item != null && !string.IsNullOrEmpty(item.ListId))
                return item.ListId;

            return string.Empty;
        }

        public string GetLinkIdByGid(string gid)
        {
            var item = Load().FirstOrDefault(e => e.Gid.Equals(gid));

            if (item != null && !string.IsNullOrEmpty(item.LinkId))
                return item.LinkId;

            return string.Empty;
        }

        public void SaveLinkId(string linkId, string listId, string url, string gid)
        {
            var list = Load().ToList();

            Save(list.Except(list.Where(e => e.Gid == gid))
                     .Concat(new[] { new LinkIdPersistenceModel(listId, linkId, url, gid) }));
        }

        public void RemoveLinkId(string gid)
        {
            var list = Load().ToList();

            Save(list.Except(list.Where(e => e.Gid == gid)));
        }
    }
}

