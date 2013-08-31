using Pagansoft.Aria2.XmlRpc;

namespace Pagansoft.Aria2.Core
{
    public class GlobalStats : IGlobalStats
    {
        public static GlobalStats From(GlobalStatResponse response)
        {
            return new GlobalStats {
                DownloadSpeed = long.Parse(response.DownloadSpeed),
                UploadSpeed = long.Parse(response.UploadSpeed),
                NumActive = int.Parse(response.NumActive),
                NumWaiting = int.Parse(response.NumWaiting),
                NumStopped = int.Parse(response.NumStopped)
            };
        }

        public long DownloadSpeed { get; private set; }

        public long UploadSpeed { get; private set; }

        public int NumActive { get; private set; }

        public int NumWaiting { get; private set; }

        public int NumStopped { get; private set; }
    }
}
