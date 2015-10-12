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

            Run(args);

            _logger.LogInfo("Application run finished.");
        }

        public static void Run(string[] args)
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
                HandleCompleted(args);
                return;
            }

            if (args.Any(a => a == "--error"))
            {
                _logger.LogInfo("Handling error event " + string.Join(" ", args));
                HandleError(args);
                return;
            }

            HandleNormalStart();

            /*
             * 1. Send Request to Homeload with &proctonew=true (only on first request)
             * 2. SetProcessing <LISTID>
             * 3. Save all LinkIds with current ListId 
             * 4. Send All URLs from LinkList to Aria with explicit GID
             * 
             * 5. Repeat From 1 every <INTERVAL> minutes
             */
        }

        private static void HandleNormalStart()
        {
            var aria = _bootstrapper.GetExport<IAria2>();
            if (!aria.Start())
            {
                _logger.LogFatal("Could not start aria");
                return;
            }

            _logger.LogInfo("Aria2 is up and running");

            var hltv = _bootstrapper.GetExport<IHltvApi>();

            hltv.GetLinks(true)
                .ContinueWith(t =>
                {
                    var links = t.Result;
                    hltv.SetProcessing(links.Id).Wait();
                    var linkIdModel = _bootstrapper.GetExport<ILinkIdRepository>();
                    var tasks = new List<Task>();

                    foreach (var linkId in links)
                    {
                        tasks.Add(
                            aria.AddUri(new[] { new Uri(linkId.Url) }) 
                                .ContinueWith(l => linkIdModel.SaveLinkId(linkId.Id, links.Id, linkId.Url, l.Result.Value)));
                    }
                        
                    Task.WhenAll(tasks).Wait();
                })
                .Wait();
        }

        private static void HandleCompleted(string[] args)
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
                    hltv.SetState(linkId, LinkState.Finished)
                        .ContinueWith(t =>
                        {
                            if (t.Result)
                            {
                                storage.RemoveLinkId(gid);
                                try
                                {
                                    aria.RemoveDownloadResult(gid).Wait();
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
                        }).Wait();
                }
                else
                {
                    try
                    {
                        aria.RemoveDownloadResult(gid)
                            .ContinueWith(_ => _logger.LogDebug("Removed gid #" + gid))
                            .Wait();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error removing download result!");
                    }
                }

                ShutdownAriaIfNoLinksLeft(storage);
            }
        }

        private static void HandleError(string[] args)
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

                hltv.SetError(linkId)
                    .ContinueWith(t => {
                        if (t.Result)
                            storage.RemoveLinkId(gid);
                    })
                    .Wait();

                ShutdownAriaIfNoLinksLeft(storage);
            }
        }

        private static void ShutdownAriaIfNoLinksLeft(ILinkIdRepository storage)
        {
            var aria = _bootstrapper.GetExport<IAria2>();
            var task = aria.GetGlobalStat();
            task.Wait();

            var status = task.Result;
            var linksInAria = status.NumActive + status.NumStopped + status.NumWaiting;

            if (linksInAria == 0 && storage.LinkCount == 0)
            {
                _logger.LogDebug("No links left in storage. Shutting aria down...");
                aria.Shutdown().Wait();
            }
        }
            
    }
}
