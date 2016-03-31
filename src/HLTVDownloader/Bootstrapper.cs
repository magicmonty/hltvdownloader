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

        private static Bootstrapper _instance = null;
        private static string _currentClassName = null;

        public static void Initialize(string className)
        {
            if (_instance != null && string.Equals(className, _currentClassName, StringComparison.Ordinal))
                return;

            _instance = new Bootstrapper();
            _instance.InternalInitialize(className);
            _currentClassName = className;
        }

        private void InternalInitialize(string className)
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

        public static T GetExport<T>()
        {
            return _instance == null
                ? default(T)
                : _instance.InternalGetExport<T>();
        }

        private T InternalGetExport<T>()
        {
            var export = _iocContainer.GetExport<T>();
            return export != null ? export.Value : default(T);
        }
    }
}

