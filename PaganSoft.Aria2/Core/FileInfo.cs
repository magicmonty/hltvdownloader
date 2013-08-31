using System.Collections.Generic;

namespace Pagansoft.Aria2.Core
{
    public struct FileInfo
    {
        public int Index;
        public string Path;
        public long Length;
        public long CompletedLength;
        public bool Selected;
        public IEnumerable<UriStatus> Uris;
    }
}