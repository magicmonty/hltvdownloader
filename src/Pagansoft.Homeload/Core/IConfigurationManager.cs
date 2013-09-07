using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Pagansoft.Homeload.Core
{
    public interface IConfigurationManager
    {
        KeyValueConfigurationCollection AppSettings { get; }

        void Save();
    }
}

