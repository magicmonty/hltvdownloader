using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using Pagansoft.Homeload.Core;
using Pagansoft.Aria2;
using System;
using NLog;

namespace PaganSoft.HLTVDownloader
{
    public class Bootstrapper
    {
        private CompositionContainer _iocContainer = null;

        public void Initialize(string className)
        {
            var types = new [] {
                typeof(Configuration),
                typeof(Aria2),
                typeof(Api),
                typeof(LinkIdRepository),
                typeof(ConfigurationManager),
                typeof(HltcHttpService),
                typeof(UrlBuilder),
                typeof(XmlStorage),
            };
            var catalog = new TypeCatalog(types);

            var cb = new CompositionBatch();
            cb.AddExportedValue<Pagansoft.Logging.ILogger>(new NLogLogger(className));

            _iocContainer = new CompositionContainer(catalog);
            _iocContainer.Compose(cb);
        }

        public T GetExport<T>()
        {
            return _iocContainer.GetExport<T>().Value;
        }
    }
}

