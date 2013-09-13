using System.Collections.Generic;

namespace Pagansoft.Aria2.Core
{
    public interface IFileInfo
    {
        int Index { get; }

        string Path { get; }

        long Length { get; }

        long CompletedLength { get; }

        bool Selected { get; }

        IEnumerable<IUriStatus> Uris { get; }
    }
}
