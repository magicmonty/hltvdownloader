using System;
using System.Collections.Generic;
using System.Linq;
using Pagansoft.Aria2;
using Pagansoft.Aria2.Core;
using Pagansoft.Homeload.Core;

namespace PaganSoft.HLTVDownloader
{
    class MainClass
    {
        static Bootstrapper _bootstrapper;

        public static void Main(string[] args)
        {
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
                HandleCompleted(args);
                return;
            }

            if (args.Any(a => a == "--error"))
            {
                HandleError(args);
                return;
            }

            var aria = _bootstrapper.GetExport<IAria2>();

            if (!aria.Start())
                return;

            Console.Out.WriteLine("Aria2 is up and running");

            var hltv = _bootstrapper.GetExport<IHltvApi>();
            var task = hltv.GetLinks(true);
            task.Wait();
            var links = task.Result;

            var ptask = hltv.SetProcessing(links.Id);
            ptask.Wait();

            var linkIdModel = _bootstrapper.GetExport<ILinkIdModel>();
            foreach (var linkId in links)
            {
                var gid = aria.AddUri(new [] { new Uri(linkId.Url) });

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

        static void HandleCompleted(string[] args)
        {
            var ariaArgs = args.SkipWhile(a => a != "--completed")
                               .Skip(1)
                               .ToList();
            if (ariaArgs.Count == 3)
            {
                var hltv = _bootstrapper.GetExport<IHltvApi>();
                var storage = _bootstrapper.GetExport<ILinkIdModel>();

                var gid = ariaArgs[0];
                var linkId = storage.GetLinkIdByGid(gid);

                var task = hltv.SetState(linkId, LinkState.Finished);
                task.Wait();

                if (task.Result)
                    storage.RemoveLinkId(gid);
            }
        }

        static void HandleError(string[] args)
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

                var task = hltv.SetError(linkId);
                task.Wait();

                if (task.Result)
                    storage.RemoveLinkId(gid);
            }
        }
    }
}
