using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(ILinkIdModel))]
    public class LinkIdModel : ILinkIdModel
    {
        private IStorage _storage;
        private object _lock;

        public LinkIdModel(IStorage storage)
        {
            _storage = storage;
            _lock = new object();
        }

        private IDictionary<string, IEnumerable<string>> Load()
        {
            lock (_lock) {
                return _storage.LoadLinks();
            }
        }

        private void Save(IDictionary<string, IEnumerable<string>> list)
        {
            lock (_lock) {
                _storage.SaveLinks(list);
            }
        }

        public string GetListIdByLinkId(string linkId)
        {
            var list = Load();
            foreach (var key in list.Keys) {
                var links = list[key];
                if (links.Any(l => l == linkId))
                    return key;
            }

            return string.Empty;
        }

        public void SaveLinkId(string linkId, string listId)
        {
            var list = Load();

            if (list.ContainsKey(listId)) {
                list[listId] = list[listId].Union(new [] { linkId });
            }
            else
                list.Add(listId, new [] { linkId });

            Save(list);
        }

        public void RemoveLinkId(string linkId)
        {
            var list = Load();
            var listId = GetListIdByLinkId(linkId);
            if (!string.IsNullOrEmpty(listId)) {
                var links = list[listId].Except(new [] { linkId });

                if (!links.Any())
                    list.Remove(listId);
                else
                    list[listId] = links;

                Save(list);
            }
        }
    }
}

