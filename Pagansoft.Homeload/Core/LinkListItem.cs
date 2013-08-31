using System;

namespace Pagansoft.Homeload.Core
{
    public class LinkListItem
    {
        public LinkListItem(string url, string id)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(url.Trim()))
                throw new ArgumentException("Url is missing");

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(id.Trim()))
                throw new ArgumentException("Url is missing");

            Url = url;
            Id = id;
        }

        public string Url { get; private set; }

        public string Id { get; private set; }
    }
}

