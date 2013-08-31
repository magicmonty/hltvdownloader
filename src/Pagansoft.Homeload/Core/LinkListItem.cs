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

        public static LinkListItem Parse(string response)
        {
            var parts = response.Split(';');
            if (parts.Length >= 2) {
                return new LinkListItem(parts[0], parts[1]);
            }

            return null;
        }
    }
}
