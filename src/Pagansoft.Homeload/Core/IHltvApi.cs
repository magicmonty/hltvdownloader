using System;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace Pagansoft.Homeload.Core
{
    public interface IHltvApi
    {
        Task<LinkList> GetLinks();

        Task<LinkList> GetLinks(bool initial);

        Task<bool> SetProcessing(string listId);

        Task<bool> SetState(string linkId, string listId, LinkState state);

        Task<bool> SetError(string linkId, string listId);
    }
}
