using System.Collections.Generic;

namespace Pagansoft.Homeload.Core
{
    public interface IStorage
    {
        IEnumerable<LinkIdPersistenceModel> LoadLinks();

        void SaveLinks(IEnumerable<LinkIdPersistenceModel> links);

        void Lock();

        void Release();
    }
}

