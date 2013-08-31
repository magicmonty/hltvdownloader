using Pagansoft.Aria2.XmlRpc;

namespace Pagansoft.Aria2.Core
{
    public class UriStatus : IUriStatus
    {
        public static UriStatus From(UriResponse response)
        {
            return new UriStatus {
                Uri = response.Uri,
                Status = response.Status
            };
        }

        public string Uri { get; private set; }

        public string Status { get; private set; }
    }
}
