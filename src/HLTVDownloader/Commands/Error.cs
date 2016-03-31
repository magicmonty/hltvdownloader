using System.Linq;
using System.Threading.Tasks;

namespace PaganSoft.HLTVDownloader.Commands
{
    public class Error : BaseCommand
    {
        private readonly string _gid;

        private Error(string gid)
        {
            _gid = gid;
        }

        public override async Task Execute()
        {
            var linkId = Storage.GetLinkIdByGid(_gid);

            await Growl.Notify($"Error on Link ${linkId}");

            if (!await Hltv.SetError(linkId))
            {
                await ShutdownAriaIfNoLinksLeft();
                return;
            }

            Storage.RemoveLinkId(_gid);
            await RemoveDownloadResult(_gid);
        }

        public static Error Create(string[] commandLineArguments)
        {
            var joinedArgs = string.Join(" ", commandLineArguments);
            LoggerManager.Info($"Handling error event {joinedArgs}");

            var errorArgs = commandLineArguments
                .SkipWhile(a => a != "--error")
                .Skip(1)
                .ToArray();

            return errorArgs.Length > 0
                ? new Error(errorArgs [0])
                : null;
        }
    }
}