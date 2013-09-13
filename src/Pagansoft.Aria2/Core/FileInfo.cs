using System.Collections.Generic;
using System.Linq;
using Pagansoft.Aria2.XmlRpc;

namespace Pagansoft.Aria2.Core
{
    public class FileInfo : IFileInfo
    {
        public static FileInfo From(FileResponse response)
        {
            return new FileInfo {
                Index = response.Index,
                Path = response.Path,
                Length = long.Parse(response.Length),
                CompletedLength = long.Parse(response.CompletedLength),
                Selected = bool.Parse(response.Selected),
                Uris = response.Uris.Select(UriStatus.From)
            };
        }

        public int Index { get; private set; }

        public string Path { get; private set; }

        public long Length { get; private set; }

        public long CompletedLength { get; private set; }

        public bool Selected { get; private set; }

        public IEnumerable<IUriStatus> Uris { get; private set; }
    }
}
