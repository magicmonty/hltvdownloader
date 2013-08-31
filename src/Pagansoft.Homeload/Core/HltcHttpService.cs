using System.Net;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pagansoft.Homeload.Core
{
    class HltcHttpService : IHLTCHttpService
    {
        public string SendGetRequest(string url)
        {
            var request = WebRequest.Create(url);
            var response = request.GetResponse().GetResponseStream();

            var result = new StringBuilder();
            StreamReader objReader = new StreamReader(response);
            string line = string.Empty;
            int i = 0;
            while (line != null) {
                i++;
                line = objReader.ReadLine();
                if (line != null)
                    result.AppendLine(line);
            }
            return result.ToString();
        }
    }
}
