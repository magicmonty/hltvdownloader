using System;
using System.Threading.Tasks;
using Pagansoft.Homeload.Core;

namespace PaganSoft.HLTVDownloader.Commands
{
    public class MissingCredentials : IAsyncCommand
    {
        private readonly IConfiguration _config;

        public MissingCredentials()
        {
            _config = Bootstrapper.GetExport<IConfiguration>();
        }

        public async Task Execute()
        {
            Console.Write("Enter your HLTV username: ");
            var user = (Console.ReadLine() ?? string.Empty).Trim();

            Console.Write("Enter your HLTV password: ");
            var password = (Console.ReadLine() ?? string.Empty).Trim();

            await Task.Run(() => _config.SaveUserNameAndPassword(user, password));
            Console.Write("Please restart the application");
        }
    }
}