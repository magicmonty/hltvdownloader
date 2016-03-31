using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Pagansoft.Homeload.Core
{
    public class LinkList : IEnumerable<LinkListItem>
    {
        private readonly List<LinkListItem> _items;

        public LinkList()
        {
            _items = new List<LinkListItem>();
            Id = string.Empty;
            HappyHourStart = null;
            HappyHourEnd = null;
            Error = string.Empty;
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

        public int LinkCount => _items.Count;

        public string Id { get; private set; }

        public int? HappyHourStart { get; private set; }

        public int? HappyHourEnd { get; private set; }

        public string Error { get; private set; }

        public int NumberOfLinks { get; private set; }

        public int Interval { get; private set; }

        private static readonly Regex ErrorRegex = new Regex(@"^(?<error>USER_NOT_FOUND|WRONG_PASSWORD|DB_ERROR|NOT_ALLOWED|NO_NEW_LINKS)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex ResponseRegex = new Regex(@"^INTERVAL=(?<int>\d+);NUMBER_OF_LINKS=(?<nol>\d+);LIST=(?<id>.*);LINKCOUNT=(?<lc>\d+);HHSTART=(?<hhs>\d+);HHEND=(?<hhe>\d+);", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static LinkList Parse(string response)
        {
            var match = ErrorRegex.Match(response);

            if (match.Success)
                return new LinkList { Error = match.Groups["error"].Value };

            var result = new LinkList();

            var lines = response.Split('\n');

            var header = lines.FirstOrDefault() ?? string.Empty;

            match = ResponseRegex.Match(header);
            if (!match.Success)
                return new LinkList { Error = "Parser error" };

            result.Interval = int.Parse(match.Groups["int"].Value);
            result.NumberOfLinks = int.Parse(match.Groups["nol"].Value);
            result.Id = match.Groups["id"].Value;
            result.HappyHourStart = int.Parse(match.Groups["hhs"].Value);
            result.HappyHourEnd = int.Parse(match.Groups["hhe"].Value);

            var links = lines.Skip(1);

            result._items.AddRange(links.Select(LinkListItem.Parse).Where(i => i != null));

            return result;
        }
    }
}

