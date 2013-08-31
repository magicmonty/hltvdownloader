namespace Pagansoft.Aria2.Options.Enums
{
    public interface ICheckSumOption
    {
        string Type { get; set; }

        string Checksum { get; set; }
    }
}
