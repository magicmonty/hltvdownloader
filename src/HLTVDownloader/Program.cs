using Pagansoft.Aria2.XmlRpc;
using Pagansoft.Aria2;
using System;
using System.Threading;

namespace PaganSoft.HLTVDownloader
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /* 
             * already Tested:
             * 
             * var aria2 = new Aria2();
             * aria2.PurgeDownloadResult();
             * aria2.AddUri(new [] { new Uri("http://blog.pagansoft.de/md5sums.txt") });
             * aria2.ForceShutdown();
             * aria2.Shutdown();
             * var stat = aria2.GetGlobalStat();
             * aria2.Remove("e97e37");
             */
            var aria2 = new Aria2();
            Console.Out.WriteLine(aria2);
            var options = aria2.GetGlobalOption();
            Console.Out.WriteLine(options.MaxConnectionPerServer);
        }
    }
}
