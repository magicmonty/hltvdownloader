using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Text;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IHLTCHttpService))]
    public class HltcHttpService : IHLTCHttpService
    {
        public string SendGetRequest(string url)
        {
            var request = WebRequest.Create(url);
            var response = request.GetResponse().GetResponseStream();

            var result = new StringBuilder();
            var objReader = new StreamReader(response);
            string line = string.Empty;
            int i = 0;
            while (line != null)
            {
                i++;
                line = objReader.ReadLine();
                if (line != null)
                    result.AppendLine(line);
            }
            return result.ToString();
        }
    }
}
