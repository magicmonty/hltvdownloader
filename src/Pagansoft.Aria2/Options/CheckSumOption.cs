namespace Pagansoft.Aria2.Options.Enums
{
    public class CheckSumOption : ICheckSumOption
    {
        public static CheckSumOption From(string checkSum)
        {
            var values = (checkSum ?? string.Empty).Split('=');
            if (values.Length == 2) {
                return new CheckSumOption { Type = values[0], Checksum = values[1] };
            }

            return null;
        }

        public string Type { get; set; }

        public string Checksum { get; set; }
    }
}
