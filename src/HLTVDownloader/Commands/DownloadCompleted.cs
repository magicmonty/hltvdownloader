using System.Linq;
using System.Threading.Tasks;
using Pagansoft.Homeload.Core;

namespace PaganSoft.HLTVDownloader.Commands
{
    public class DownloadCompleted : BaseCommand
    {
        private readonly string _gid;
        private readonly string _path;

        private DownloadCompleted(string gid, string path)
        {
            _gid = gid;
            _path = path;
        }

        public override async Task Execute()
        {
            var linkId = Storage.GetLinkIdByGid(_gid);

            if (!string.IsNullOrEmpty(_path))
                await Growl.Notify($"Download complete!\n{_path}");

            if (string.IsNullOrEmpty(linkId))
            {
                await RemoveDownloadResult(_gid);
                return;
            }

            if (!await Hltv.SetState(linkId, LinkState.Finished))
            {
                LoggerManager.Error("Error from server for setting link {0} to finished", linkId);
                await ShutdownAriaIfNoLinksLeft();
                return;
            }

            Storage.RemoveLinkId(_gid);
            await RemoveDownloadResult(_gid);
        }

        public static DownloadCompleted Create(string[] commandLineArguments)
        {
            var joinedArgs = string.Join(" ", commandLineArguments);
            LoggerManager.Info($"Handling completed event {joinedArgs}");

            var completionArgs = commandLineArguments
                .SkipWhile(a => a != "--completed")
                .Skip(1)
                .ToArray();

            return completionArgs.Length > 2
                ? new DownloadCompleted(completionArgs[0], completionArgs[2])
                : null;
        }
    }
}