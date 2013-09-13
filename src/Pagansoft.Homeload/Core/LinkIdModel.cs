using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(ILinkIdModel))]
    public class LinkIdModel : ILinkIdModel
    {
        private IStorage _storage;
        private object _lock;

        [ImportingConstructor]
        public LinkIdModel(IStorage storage)
        {
            _storage = storage;
            _lock = new object();
        }

        private IEnumerable<LinkIdPersistenceModel> Load()
        {
            lock (_lock)
            {
                return _storage.LoadLinks();
            }
        }

        private void Save(IEnumerable<LinkIdPersistenceModel> list)
        {
            lock (_lock)
            {
                _storage.SaveLinks(list);
            }
        }

        public string GetListIdByGid(string gid)
        {
            var item = Load().FirstOrDefault(e => e.Gid == gid);

            if (!string.IsNullOrEmpty(item.ListId))
                return item.ListId;

            return string.Empty;
        }

        public string GetLinkIdByGid(string gid)
        {
            var item = Load().FirstOrDefault(e => e.Gid == gid);

            if (!string.IsNullOrEmpty(item.LinkId))
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
            var list = Load();

            Save(list.Except(Enumerable.Where(list, e => e.Gid == gid)));
        }
    }
}

