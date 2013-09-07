using System.Collections.Generic;

namespace Pagansoft.Homeload.Core
{
    public interface IStorage
    {
        IDictionary<string, IEnumerable<string>> LoadLinks();

        void SaveLinks(IDictionary<string, IEnumerable<string>> links);
    }
}

