namespace Pagansoft.Homeload.Core
{
    public interface ILinkIdModel
    {
        string GetListIdByGid(string gid);

        string GetLinkIdByGid(string gid);

        void SaveLinkId(string linkId, string listId, string url, string gid);

        void RemoveLinkId(string gid);

        int LinkCount { get; }
    }
}

