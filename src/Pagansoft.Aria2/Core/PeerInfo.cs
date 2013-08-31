using Pagansoft.Aria2.XmlRpc;

namespace Pagansoft.Aria2.Core
{
    public class PeerInfo : IPeerInfo
    {
        public static PeerInfo From(PeerResponse response)
        {
            return new PeerInfo {
                PeerId = response.PeerId,
                Ip = response.Ip,
                Port = int.Parse(response.Port),
                BitField = response.BitField,
                AmChoking = bool.Parse(response.AmChoking),
                PeerChoking = bool.Parse(response.PeerChoking),
                DownloadSpeed = long.Parse(response.DownloadSpeed),
                UploadSpeed = long.Parse(response.UploadSpeed),
                Seeder = bool.Parse(response.Seeder)
            };
        }

        public string PeerId { get; private set; }

        public string Ip { get; private set; }

        public int Port { get; private set; }

        public string BitField { get; private set; }

        public bool AmChoking { get; private set; }

        public bool PeerChoking { get; private set; }

        public long DownloadSpeed { get; private set; }

        public long UploadSpeed { get; private set; }

        public bool Seeder { get; private set; }
    }
}
