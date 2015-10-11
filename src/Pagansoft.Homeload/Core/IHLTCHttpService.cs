using System.Threading.Tasks;

namespace Pagansoft.Homeload.Core
{
    public interface IHLTCHttpService
    {
        Task<string> SendGetRequest(string url);
    }
}

