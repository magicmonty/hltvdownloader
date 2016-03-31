using System;
using System.Threading.Tasks;
using Pagansoft.Aria2;
using Pagansoft.Homeload.Core;

namespace PaganSoft.HLTVDownloader.Commands
{
    public abstract class BaseCommand : IAsyncCommand
    {
        protected BaseCommand()
        {
            Aria = Bootstrapper.GetExport<IAria2>();
            Storage = Bootstrapper.GetExport<ILinkIdRepository>();
            Hltv = Bootstrapper.GetExport<IHltvApi>();
        }

        public abstract Task Execute();

        protected ILinkIdRepository Storage { get; }

        protected IAria2 Aria { get; }

        protected IHltvApi Hltv { get; }

        protected async Task RemoveDownloadResult(string gid)
        {
            try
            {
                await Aria.RemoveDownloadResult(gid);
                LoggerManager.Debug($"Removed gid #{gid}");
            }
            catch (Exception ex)
            {
                LoggerManager.Error(ex, "Error removing download result!");
            }
            finally
            {
                await ShutdownAriaIfNoLinksLeft();
            }
        }

        protected async Task ShutdownAriaIfNoLinksLeft()
        {
            var status = await Aria.GetGlobalStat();

            var linksInAria = status.NumActive + status.NumStopped + status.NumWaiting;

            if (linksInAria != 0 || Storage.LinkCount != 0)
                return;

            LoggerManager.Debug("No links left in storage. Shutting aria down...");
            await Aria.Shutdown();
        }
    }
}