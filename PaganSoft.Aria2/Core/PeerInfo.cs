namespace PaganSoft.Aria2.Core
{

    public struct PeerInfo
    {
        public string PeerId;
        public string Ip;
        public int Port;
        public string BitField;
        public bool AmChoking;
        public bool PeerChoking;
        public long DownloadSpeed;
        public long UploadSpeed;
        public bool Seeder;
    }
    
}
