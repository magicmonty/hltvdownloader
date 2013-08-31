using System;
using System.Collections.Generic;
using System.Collections;

namespace Pagansoft.Homeload.Core
{
    public class LinkList : IEnumerable<LinkListItem>
    {
        private List<LinkListItem> _items;

        public LinkList()
        {
            _items = new List<LinkListItem>();
        }
        #region IEnumerable implementation
        public IEnumerator<LinkListItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
        #endregion
    }
}

