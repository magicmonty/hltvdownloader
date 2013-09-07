using System.Collections.Specialized;
using System.ComponentModel.Composition;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IConfigurationManager))]
    public class ConfigurationManager : IConfigurationManager
    {
        public NameValueCollection AppSettings
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings;
            }
        }
    }
}

