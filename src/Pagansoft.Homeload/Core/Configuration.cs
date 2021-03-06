using System;
using System.ComponentModel.Composition;
using System.IO;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IConfiguration))]
    public class Configuration : IConfiguration
    {
        readonly IConfigurationManager _configurationManager;

        [ImportingConstructor]
        public Configuration(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        public string HltvUserName
        {
            get
            {
                var userNameSetting = _configurationManager.AppSettings["username"];
                return userNameSetting == null 
                    ? string.Empty 
                    : _configurationManager.AppSettings["username"].Value;

            }
        }

        public string HltvPassword
        {
            get
            {
                var userNameSetting = _configurationManager.AppSettings["password"];
                return userNameSetting == null 
                    ? string.Empty 
                    : _configurationManager.AppSettings["password"].Value;

            }
        }

        public string ConfigurationDirectory
        {
            get
            {
                var result = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    ".hltc");

                if (!Directory.Exists(result))
                    Directory.CreateDirectory(result);

                return result;
            }
        }

        public void SaveUserNameAndPassword(string user, string password)
        {
            var setting = _configurationManager.AppSettings["username"];
            if (setting != null)
                _configurationManager.AppSettings["username"].Value = user;
            else
                _configurationManager.AppSettings.Add("username", user);

            setting = _configurationManager.AppSettings["password"];
            if (setting != null)
                _configurationManager.AppSettings["password"].Value = user;
            else
                _configurationManager.AppSettings.Add("password", password);

            _configurationManager.Save();
        }
    }
}

