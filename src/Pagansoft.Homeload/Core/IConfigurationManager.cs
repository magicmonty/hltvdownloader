using System;
using System.Collections.Specialized;

namespace Pagansoft.Homeload.Core
{
    public interface IConfigurationManager
    {
        NameValueCollection AppSettings { get; }
    }
}

