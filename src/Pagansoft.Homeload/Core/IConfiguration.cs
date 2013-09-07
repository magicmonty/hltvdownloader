namespace Pagansoft.Homeload.Core
{
    public interface IConfiguration
    {
        string HltvUserName { get; }

        string HltvPassword { get; }

        string ConfigurationDirectory { get; }
    }
}

