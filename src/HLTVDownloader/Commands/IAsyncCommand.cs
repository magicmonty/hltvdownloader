using System.Threading.Tasks;

namespace PaganSoft.HLTVDownloader.Commands
{
    public interface IAsyncCommand
    {
        Task Execute();
    }
}