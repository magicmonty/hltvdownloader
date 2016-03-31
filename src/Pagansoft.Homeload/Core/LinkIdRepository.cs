using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System;

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

        public int LinkCount => Load().Count();

        private IEnumerable<LinkIdPersistenceModel> Load()
        {
            lock (_lock)
            {
                return _storage.LoadLinks() ?? Enumerable.Empty<LinkIdPersistenceModel>();
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
            return Locked(() =>
            {
                return Load()
                    .FirstOrDefault(e => e.Gid == gid)
                    ?.ListId ?? string.Empty;
            });
        }

        public string GetLinkIdByGid(string gid)
        {
            return Locked(() => {
                return Load()
                    .FirstOrDefault(e => e.Gid.Equals(gid))
                    ?.LinkId ?? string.Empty;
            });
        }

        public void SaveLinkId(string linkId, string listId, string url, string gid)
        {
            Locked(() =>
            {
                var list = Load().ToList();

                Save(list.Except(list.Where(e => e.Gid == gid))
                    .Concat(new[] { new LinkIdPersistenceModel(listId, linkId, url, gid) }));
            });
        }

        public void RemoveLinkId(string gid)
        {
            Locked(() =>
            {
                var list = Load().ToList();
                Save(list.Except(list.Where(e => e.Gid == gid)));
            });
        }

        private string Locked(Func<string> f)
        {
            lock (_lock)
            {
                _storage.Lock();
                try
                {
                    return f();
                }
                finally {
                    _storage.Release();
                }
            }
        }

        private void Locked(Action f)
        {
            lock (_lock)
            {
                _storage.Lock();
                try
                {
                    f();
                }
                finally {
                    _storage.Release();
                }
            }
        }
    }
}

