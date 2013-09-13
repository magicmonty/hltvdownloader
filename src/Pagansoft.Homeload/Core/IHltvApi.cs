using System.Threading.Tasks;

namespace Pagansoft.Homeload.Core
{
    public interface IHltvApi
    {
        Task<LinkList> GetLinks();

        Task<LinkList> GetLinks(bool initial);

        Task<bool> SetProcessing(string listId);

        Task<bool> SetState(string linkId, LinkState state);

        Task<bool> SetError(string linkId);
    }
}
