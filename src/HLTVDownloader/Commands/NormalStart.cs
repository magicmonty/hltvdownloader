using System;
using System.Threading.Tasks;

namespace PaganSoft.HLTVDownloader.Commands
{
    /*
     * 1. Send Request to Homeload with &proctonew=true (only on first request)
     * 2. SetProcessing <LISTID>
     * 3. Save all LinkIds with current ListId
     * 4. Send All URLs from LinkList to Aria with explicit GID
     *
     * 5. Repeat From 1 every <INTERVAL> minutes
     */
    public class NormalStart : BaseCommand
    {
        public override async Task Execute()
        {
            if (!await Aria.Start())
            {
                LoggerManager.Fatal("Could not start aria");
                return;
            }

            LoggerManager.Info("Aria2 is up and running");

            var links = await Hltv.GetLinks(true);
            await Hltv.SetProcessing(links.Id);

            foreach (var linkId in links)
            {
                try
                {
                    var result = await Aria.AddUri(new[] {new Uri(linkId.Url)});
                    Storage.SaveLinkId(linkId.Id, links.Id, linkId.Url, result.Value);
                }
                catch (Exception e)
                {
                    LoggerManager.Error(e, "Error adding URI and saving linkId");
                    continue;
                }
            }
        }
    }
}