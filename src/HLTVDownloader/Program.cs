using Pagansoft.Aria2.XmlRpc;
using Pagansoft.Aria2;
using System;
using System.Threading;
using System.Linq;
using Pagansoft.Homeload.Core;

namespace PaganSoft.HLTVDownloader
{
    class MainClass
    {
        private static Bootstrapper _bootstrapper;

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

                var linkId = args[0];
                var listId = storage.GetListIdByLinkId(linkId);

                var task = hltv.SetState(listId, linkId, LinkState.Finished);
                task.Wait();

                if (task.Result)
                    storage.RemoveLinkId(linkId);
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

                var linkId = args[0];
                var listId = storage.GetListIdByLinkId(linkId);

                var task = hltv.SetError(listId, linkId);
                task.Wait();

                if (task.Result)
                    storage.RemoveLinkId(linkId);
            }
        }
    }
}
