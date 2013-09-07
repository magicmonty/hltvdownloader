namespace Pagansoft.Homeload.Core
{
    public interface ILinkIdModel
    {
        string GetListIdByLinkId(string linkId);

        void SaveLinkId(string linkId, string listId);

        void RemoveLinkId(string linkId);
    }
}

