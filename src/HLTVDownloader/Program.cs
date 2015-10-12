using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pagansoft.Logging;
using Pagansoft.Aria2;
using Pagansoft.Aria2.Core;
using Pagansoft.Homeload.Core;

namespace PaganSoft.HLTVDownloader
{
    class MainClass
    {
        static Bootstrapper _bootstrapper;
        static ILogger _logger;
        static readonly Growl _growl = new Growl();

        public static void Main(string[] args)
        {
            _bootstrapper = new Bootstrapper();
            _bootstrapper.Initialize("PaganSoft.HLTVDownloader");
            _logger = _bootstrapper.GetExport<ILogger>();

            _logger.LogInfo("Starting application");

            Run(args).Wait();

            _logger.LogInfo("Application run finished.");
        }

        public static async Task Run(string[] args)
        {
            var config = _bootstrapper.GetExport<IConfiguration>();
            if (string.IsNullOrEmpty(config.HltvUserName) || string.IsNullOrEmpty(config.HltvPassword))
            {
                Console.Write("Enter your HLTV username: ");
                var user = Console.ReadLine().Trim();
                Console.Write("Enter your HLTV password: ");
                var password = Console.ReadLine().Trim();

                config.SaveUserNameAndPassword(user, password);

                Console.Write("Please restart the application");

                return;
            }

            if (args.Any(a => a == "--completed"))
            {
                _logger.LogInfo("Handling completed event " + string.Join(" ", args));
                await HandleCompleted(args);
                return;
            }

            if (args.Any(a => a == "--error"))
            {
                _logger.LogInfo("Handling error event " + string.Join(" ", args));
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
            if (!aria.Start())
            {
                _logger.LogFatal("Could not start aria");
                return;
            }

            _logger.LogInfo("Aria2 is up and running");

            var hltv = _bootstrapper.GetExport<IHltvApi>();

            var links = await hltv.GetLinks(true);
            await hltv.SetProcessing(links.Id);

            var linkIdModel = _bootstrapper.GetExport<ILinkIdRepository>();
            var tasks = new List<Task>();

            foreach (var linkId in links)
            {
                tasks.Add(
                    aria.AddUri(new[] { new Uri(linkId.Url) }) 
                                .ContinueWith(l => linkIdModel.SaveLinkId(linkId.Id, links.Id, linkId.Url, l.Result.Value)));
            }
                        
            await Task.WhenAll(tasks);
            return;
        }

        private static async Task HandleCompleted(string[] args)
        {
            var ariaArgs = args.SkipWhile(a => a != "--completed")
                               .Skip(1)
                               .ToList();
            if (ariaArgs.Count == 3)
            {
                var hltv = _bootstrapper.GetExport<IHltvApi>();
                var storage = _bootstrapper.GetExport<ILinkIdRepository>();

                var gid = ariaArgs[0];
                var path = ariaArgs[2];
                var linkId = storage.GetLinkIdByGid(gid);
                var aria = _bootstrapper.GetExport<IAria2>();

                if (!string.IsNullOrEmpty(path))
                    _growl.Notify(path);

                if (!string.IsNullOrEmpty(linkId))
                {
                    var result = await hltv.SetState(linkId, LinkState.Finished);

                    if (result)
                    {
                        storage.RemoveLinkId(gid);
                        try
                        {
                            await aria.RemoveDownloadResult(gid);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error removing download result!");
                        }
                    }
                    else
                    {
                        _logger.Error("Error from server for setting link {0} to finished", linkId);
                    }
                }
                else
                {
                    try
                    {
                        await aria.RemoveDownloadResult(gid);
                        _logger.LogDebug("Removed gid #" + gid);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error removing download result!");
                    }
                }

                await ShutdownAriaIfNoLinksLeft(storage);
            }
        }

        private static async Task HandleError(string[] args)
        {
            var ariaArgs = args.SkipWhile(a => a != "--error")
                               .Skip(1)
                               .ToList();

            if (ariaArgs.Count == 3)
            {
                var hltv = _bootstrapper.GetExport<IHltvApi>();
                var storage = _bootstrapper.GetExport<ILinkIdRepository>();

                var gid = ariaArgs[0];
                var linkId = storage.GetLinkIdByGid(gid);

                var result = await hltv.SetError(linkId);

                if (result)
                    storage.RemoveLinkId(gid);

                await ShutdownAriaIfNoLinksLeft(storage);
            }

            return;
        }

        private static async Task ShutdownAriaIfNoLinksLeft(ILinkIdRepository storage)
        {
            var aria = _bootstrapper.GetExport<IAria2>();
            var status = await aria.GetGlobalStat();

            var linksInAria = status.NumActive + status.NumStopped + status.NumWaiting;

            if (linksInAria == 0 && storage.LinkCount == 0)
            {
                _logger.LogDebug("No links left in storage. Shutting aria down...");
                await aria.Shutdown();
            }
            return;
        }
            
    }
}
