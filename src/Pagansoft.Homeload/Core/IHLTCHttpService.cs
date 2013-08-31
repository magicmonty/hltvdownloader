using System.Threading;
using System.Threading.Tasks;

namespace Pagansoft.Homeload.Core
{
    public interface IHLTCHttpService
    {
        string SendGetRequest(string url);
    }
}

