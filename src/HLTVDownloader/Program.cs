using System;
using System.Linq;
using System.Threading.Tasks;
using Pagansoft.Logging;
using Pagansoft.Aria2;
using Pagansoft.Aria2.Core;
using Pagansoft.Homeload.Core;
using PaganSoft.HLTVDownloader.Commands;

namespace PaganSoft.HLTVDownloader
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Bootstrapper.Initialize("PaganSoft.HLTVDownloader");

            LoggerManager.Info("Starting application");

            Task.Run(async () => await Run(args)).Wait();

            LoggerManager.Info("Application run finished.");
        }

        public static async Task Run(string[] args)
        {
            var command = GetCommand(args);
            if (command != null)
                await command.Execute();
        }

        private static IAsyncCommand GetCommand(string[] commandLineArguments)
        {
            if (IsCredentialsMissing())
                return new MissingCredentials();

            if (commandLineArguments.Any(a => string.Equals(a, "--completed", StringComparison.OrdinalIgnoreCase)))
                return DownloadCompleted.Create(commandLineArguments);

            if (commandLineArguments.Any(a => string.Equals(a, "--error", StringComparison.OrdinalIgnoreCase)))
                return Error.Create(commandLineArguments);

            return new NormalStart();
        }

        private static bool IsCredentialsMissing()
        {
            var config = Bootstrapper.GetExport<IConfiguration>();

            return string.IsNullOrEmpty(config.HltvUserName)
                   || string.IsNullOrEmpty(config.HltvPassword);
        }
    }
}
