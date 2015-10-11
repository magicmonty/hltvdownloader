using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using Pagansoft.Homeload.Core;
using Pagansoft.Aria2;
using System;

namespace PaganSoft.HLTVDownloader
{
    public class Bootstrapper
    {
        private CompositionContainer _iocContainer = null;

        public void Initialize()
        {
            var types = new [] {
                typeof(Configuration),
                typeof(Aria2),
                typeof(Api),
                typeof(LinkIdModel),
                typeof(ConfigurationManager),
                typeof(HltcHttpService),
                typeof(UrlBuilder),
                typeof(XmlStorage)
            };
            var catalog = new TypeCatalog(types);

            var cb = new CompositionBatch();
            _iocContainer = new CompositionContainer(catalog);
            _iocContainer.Compose(cb);
        }

        public T GetExport<T>()
        {
            return _iocContainer.GetExport<T>().Value;
        }
    }
}

