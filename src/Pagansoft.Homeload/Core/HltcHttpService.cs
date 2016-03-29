using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Net;
using Pagansoft.Logging;
using System.Threading.Tasks;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IHLTCHttpService))]
    public class HltcHttpService : IHLTCHttpService
    {
        [Import]
        private ILogger _logger;

        public async Task<string> SendGetRequest(string url)
        {
            _logger.LogTrace($"Sending request to {url}");

            try 
            {
                var request = WebRequest.Create(url);
                using (var response = (HttpWebResponse)await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse(null, null), request.EndGetResponse))
                {
                    if (response.ContentLength == 0)
                        return response.StatusDescription;
                    
                    var responseStream = response.GetResponseStream();
                    using (var reader = new StreamReader(responseStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            } 
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error on SendGetRequest");
                return string.Empty;
            }
        }
    }
}
