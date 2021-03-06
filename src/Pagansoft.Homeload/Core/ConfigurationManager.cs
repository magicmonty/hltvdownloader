using System.ComponentModel.Composition;
using Conf = System.Configuration;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IConfigurationManager))]
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly Conf.Configuration _configuration;

        public ConfigurationManager()
        {
            _configuration = Conf.ConfigurationManager.OpenExeConfiguration(Conf.ConfigurationUserLevel.PerUserRoaming);
        }

        public Conf.KeyValueConfigurationCollection AppSettings => _configuration.AppSettings.Settings;

        public void Save()
        {
            _configuration.Save(Conf.ConfigurationSaveMode.Modified);
        }
    }
}

