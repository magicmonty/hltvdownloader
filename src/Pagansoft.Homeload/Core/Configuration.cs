using System;
using System.ComponentModel.Composition;
using System.IO;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IConfiguration))]
    public class Configuration : IConfiguration
    {
        private readonly IConfigurationManager _configurationManager;

        [ImportingConstructor]
        public Configuration(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public string HltvUserName {
            get
            {
                return _configurationManager.AppSettings["username"];
            }
        }

        public string HltvPassword {
            get
            {
                return _configurationManager.AppSettings["password"];
            }
        }

        public string ConfigurationDirectory {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    ".hltc");
            }
        }
    }
}

