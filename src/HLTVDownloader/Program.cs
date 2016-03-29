using System;
using System.Linq;
using System.Threading.Tasks;
using Pagansoft.Logging;
using Pagansoft.Aria2;
using Pagansoft.Aria2.Core;
using Pagansoft.Homeload.Core;

namespace PaganSoft.HLTVDownloader
{
    internal class MainClass
    {
        private static Bootstrapper _bootstrapper;
        private static ILogger _logger;
        private static readonly Growl Growl = new Growl();

        public static void Main(string[] args)
        {
            _bootstrapper = new Bootstrapper();
            _bootstrapper.Initialize("PaganSoft.HLTVDownloader");
            _logger = _bootstrapper.GetExport<ILogger>();

            _logger.LogInfo("Starting application");

            Task.Run(async () => await Run(args)).Wait();

            _logger.LogInfo("Application run finished.");
        }

        public static async Task Run(string[] args)
        {
            var config = _bootstrapper.GetExport<IConfiguration>();
            if (string.IsNullOrEmpty(config.HltvUserName) || string.IsNullOrEmpty(config.HltvPassword))
            {
                Console.Write("Enter your HLTV username: ");
                var user = (Console.ReadLine() ?? string.Empty).Trim();
                Console.Write("Enter your HLTV password: ");
                var password = (Console.ReadLine() ?? string.Empty).Trim();

                config.SaveUserNameAndPassword(user, password);

                Console.Write("Please restart the application");

                return;
            }

            var joinedArgs = string.Join(" ", args);
            if (args.Any(a => a == "--completed"))
            {
                _logger.LogInfo($"Handling completed event {joinedArgs}");
                await HandleCompleted(args);
                return;
            }

            if (args.Any(a => a == "--error"))
            {
                _logger.LogInfo($"Handling error event {joinedArgs}");
                await HandleError(args);
                return;
            }

            await HandleNormalStart();

            /*
             * 1. Send Request to Homeload with &proctonew=true (only on first request)
             * 2. SetProcessing <LISTID>
             * 3. Save all LinkIds with current ListId
             * 4. Send All URLs from LinkList to Aria with explicit GID
             *
             * 5. Repeat From 1 every <INTERVAL> minutes
             */
        }

        private static async Task HandleNormalStart()
        {
            var aria = _bootstrapper.GetExport<IAria2>();
            if (!await aria.Start())
            {
                _logger.LogFatal("Could not start aria");
                return;
            }

            _logger.LogInfo("Aria2 is up and running");

            var hltv = _bootstrapper.GetExport<IHltvApi>();

            var links = await hltv.GetLinks(true);
            await hltv.SetProcessing(links.Id);

            var linkIdModel = _bootstrapper.GetExport<ILinkIdRepository>();

            foreach (var linkId in links)
            {
                try
                {
                    var result = await aria.AddUri(new[] {new Uri(linkId.Url)});
                    linkIdModel.SaveLinkId(linkId.Id, links.Id, linkId.Url, result.Value);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Error adding URI and saving linkId");
                    continue;
                }
            }
        }

        private static async Task HandleCompleted(string[] args)
        {
            var ariaArgs = args.SkipWhile(a => a != "--completed")
                               .Skip(1)
                               .ToArray();
            
            if (ariaArgs.Length < 3)
                return;
            
            var hltv = _bootstrapper.GetExport<IHltvApi>();
            var storage = _bootstrapper.GetExport<ILinkIdRepository>();

            var gid = ariaArgs[0];
            var path = ariaArgs[2];
            var linkId = storage.GetLinkIdByGid(gid);

            if (!string.IsNullOrEmpty(path))
                await Growl.Notify($"Download complete!\n{path}");

            if (string.IsNullOrEmpty(linkId))
            {
                await RemoveDownloadResult(gid);
                return;
            }

            if (!await hltv.SetState(linkId, LinkState.Finished))
            {
                _logger.Error("Error from server for setting link {0} to finished", linkId);
                await ShutdownAriaIfNoLinksLeft(storage);
                return;
            }

            storage.RemoveLinkId(gid);
            await RemoveDownloadResult(gid);
        }

        private static async Task RemoveDownloadResult(string gid)
        {
            var aria = _bootstrapper.GetExport<IAria2>();
            var storage = _bootstrapper.GetExport<ILinkIdRepository>();
            
            try
            {
                await aria.RemoveDownloadResult(gid);
                _logger.LogDebug($"Removed gid #{gid}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing download result!");
            }
            finally 
            {
                await ShutdownAriaIfNoLinksLeft(storage);
            }
        }

        private static async Task HandleError(string[] args)
        {
            var ariaArgs = args.SkipWhile(a => a != "--error")
                               .Skip(1)
                               .ToArray();

            if (ariaArgs.Length < 1)
                return;

            var hltv = _bootstrapper.GetExport<IHltvApi>();
            var storage = _bootstrapper.GetExport<ILinkIdRepository>();
            var aria = _bootstrapper.GetExport<IAria2>();

            var gid = ariaArgs[0];
            var linkId = storage.GetLinkIdByGid(gid);

            await Growl.Notify($"Error on Link ${linkId}");

            if (!await hltv.SetError(linkId))
            {
                await ShutdownAriaIfNoLinksLeft(storage);
                return;
            }

            storage.RemoveLinkId(gid);
            await RemoveDownloadResult(gid);
        }

        private static async Task ShutdownAriaIfNoLinksLeft(ILinkIdRepository storage)
        {
            var aria = _bootstrapper.GetExport<IAria2>();
            var status = await aria.GetGlobalStat();

            var linksInAria = status.NumActive + status.NumStopped + status.NumWaiting;

            if (linksInAria != 0 || storage.LinkCount != 0)
                return;

            _logger.LogDebug("No links left in storage. Shutting aria down...");
            await aria.Shutdown();
        }

    }
}
