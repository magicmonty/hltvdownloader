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
             * 1. Prüfen ob Aria läuft
             * 2. Wenn nicht, dann Aria starten
             */
            var aria = new Aria2();

            if (!aria.Start())
                return;

            Console.Out.WriteLine("Aria2 is up and running");
            aria.ForceShutdown();
        }
    }
}
