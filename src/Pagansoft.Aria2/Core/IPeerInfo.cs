namespace Pagansoft.Aria2.Core
{
    public interface IPeerInfo
    {
        string PeerId { get; }

        string Ip { get; }

        int Port { get; }

        string BitField { get; }

        bool AmChoking { get; }

        bool PeerChoking { get; }

        long DownloadSpeed { get; }

        long UploadSpeed { get; }

        bool Seeder { get; }
    }
}
