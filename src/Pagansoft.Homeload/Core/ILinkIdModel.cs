using Pagansoft.Aria2.Core;

namespace Pagansoft.Homeload.Core
{
    public interface ILinkIdModel
    {
        string GetListIdByGid(GID gid);

        string GetLinkIdByGid(GID gid);

        void SaveLinkId(string linkId, string listId, string url, GID gid);

        void RemoveLinkId(GID gid);
    }
}

