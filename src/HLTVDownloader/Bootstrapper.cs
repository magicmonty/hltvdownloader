using System;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;

namespace PaganSoft.HLTVDownloader
{
    public class Bootstrapper
    {
        private CompositionContainer _iocContainer = null;

        public void Initialize()
        {
            var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly().CodeBase);
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

