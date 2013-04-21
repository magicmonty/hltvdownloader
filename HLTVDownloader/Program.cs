using System;
using CookComputing.XmlRpc;
using PaganSoft.HLTVDownloader.Aria.XmlRpc;

namespace PaganSoft.HLTVDownloader
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var aria2c = XmlRpcProxyGen.Create<IAria2c>();
            var stats = aria2c.GetGlobalOption();
            Console.WriteLine("Version: " + stats);
        }
    }
}
