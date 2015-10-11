using System;
using System.Collections.Generic;
using System.Linq;
using Pagansoft.Aria2;
using Pagansoft.Aria2.Core;
using Pagansoft.Homeload.Core;
using NLog;

namespace PaganSoft.HLTVDownloader
{
    class MainClass
    {
        static Bootstrapper _bootstrapper;
        static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        static readonly Growl _growl = new Growl();

        static void LogDebug(string message)
        {
            if (_logger != null)
            {
                try
                {
                    _logger.Debug(message);
                }
                // Analysis disable once EmptyGeneralCatchClause
                catch
                {
                }
            }
        }

        static void LogError<T>(T message)
        {
            if (_logger != null)
            {
                try
                {
                    _logger.Error(message);
                }
                // Analysis disable once EmptyGeneralCatchClause
                catch
                {
                }
            }
        }

        public static void Main(string[] args)
        {
            new Growl().Notify("TEST");
            Console.ReadKey();
            // Run(args);
        }

        public async static void Run(string[] args)
        {
            LogDebug("Starting application");
            _bootstrapper = new Bootstrapper();
            _bootstrapper.Initialize();
           
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
                LogDebug("Handling completed event " + string.Join(" ", args));
                HandleCompleted(args);
                return;
            }

            if (args.Any(a => a == "--error"))
            {
                LogDebug("Handling error event " + string.Join(" ", args));
                HandleError(args);
                return;
            }

            var aria = _bootstrapper.GetExport<IAria2>();

            if (!aria.Start())
            {
                LogDebug("Could not start aria");
                return;
            }

            Console.Out.WriteLine("Aria2 is up and running");

            var hltv = _bootstrapper.GetExport<IHltvApi>();
            var links = await hltv.GetLinks(true);

            await hltv.SetProcessing(links.Id);

            var linkIdModel = _bootstrapper.GetExport<ILinkIdModel>();
            foreach (var linkId in links)
            {
                var gid = await aria.AddUri(new [] { new Uri(linkId.Url) });

                linkIdModel.SaveLinkId(linkId.Id, links.Id, linkId.Url, gid.Value);
            }

            /*
             * 1. Send Request to Homeload with &proctonew=true (only on first request)
             * 2. SetProcessing <LISTID>
             * 3. Save all LinkIds with current ListId 
             * 4. Send All URLs from LinkList to Aria with explicit GID
             * 
             * 5. Repeat From 1 every <INTERVAL> minutes
             */
        }

        static async void HandleCompleted(string[] args)
        {
            var ariaArgs = args.SkipWhile(a => a != "--completed")
                               .Skip(1)
                               .ToList();
            if (ariaArgs.Count == 3)
            {
                var hltv = _bootstrapper.GetExport<IHltvApi>();
                var storage = _bootstrapper.GetExport<ILinkIdModel>();

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
                            LogError(ex);
                        }
                    }
                }
                else
                {
                    try
                    {
                        await aria.RemoveDownloadResult(gid);
                        LogDebug("Removed gid #" + gid);
                    }
                    catch (Exception ex)
                    {
                        LogError(ex);
                    }
                }

                ShutdownAriaIfNoLinksLeft(storage);
            }
        }

        private static async void HandleError(string[] args)
        {
            var ariaArgs = args.SkipWhile(a => a != "--error")
                               .Skip(1)
                               .ToList();

            if (ariaArgs.Count == 3)
            {
                var hltv = _bootstrapper.GetExport<IHltvApi>();
                var storage = _bootstrapper.GetExport<ILinkIdModel>();

                var gid = ariaArgs[0];
                var linkId = storage.GetLinkIdByGid(gid);

                var result = await hltv.SetError(linkId);

                if (result)
                    storage.RemoveLinkId(gid);

                ShutdownAriaIfNoLinksLeft(storage);
            }
        }

        private async static void ShutdownAriaIfNoLinksLeft(ILinkIdModel storage)
        {
            var aria = _bootstrapper.GetExport<IAria2>();
            var status = await aria.GetGlobalStat();

            var linksInAria = status.NumActive + status.NumStopped + status.NumWaiting;

            if (linksInAria == 0 && storage.LinkCount == 0)
                await aria.Shutdown();
        }
    }
}
