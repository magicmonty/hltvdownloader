namespace Pagansoft.Aria2.Core
{
    public interface IGlobalStats
    {
        long DownloadSpeed { get; }

        long UploadSpeed { get; }

        int NumActive { get; }

        int NumWaiting { get; }

        int NumStopped { get; }
    }
}
