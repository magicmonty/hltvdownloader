using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Pagansoft.Logging;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IHLTCHttpService))]
    public class HltcHttpService : IHLTCHttpService
    {
        [Import]
        private ILogger _logger;

        public Task<string> SendGetRequest(string url)
        {
            return Task.Factory.StartNew(() =>
            {
                _logger.LogTrace("Sending request to {0}", url);

                var request = WebRequest.Create(url);
                try {
                    using (var response = request.GetResponse())
                    {
                        var responseStream = response.GetResponseStream();
                        using (var reader = new StreamReader(responseStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                } catch (Exception ex) {
                    _logger.LogError(ex, "Error on SendGetRequest");
                    return string.Empty;
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
