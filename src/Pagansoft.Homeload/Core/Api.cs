using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Pagansoft.Logging;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IHltvApi))]
    public class Api : IHltvApi
    {
        private readonly IHLTCHttpService _httpservice;
        private readonly IUrlBuilder _urlBuilder;

        [Import]
        private ILogger _logger;

        [ImportingConstructor]
        public Api(IHLTCHttpService httpservice, IUrlBuilder urlBuilder)
        {
            _httpservice = httpservice;
            _urlBuilder = urlBuilder;
        }

        public async Task<LinkList> GetLinks()
        {
            return await GetLinks(initial: false);
        }

        public async Task<LinkList> GetLinks(bool initial)
        {
            _logger.LogTrace(initial ? "Getting Links and resetting downloads to new..." : "Getting links...");

            var url = _urlBuilder.BuildGetLinksUrl(initial);

            var request = await _httpservice.SendGetRequest(url);
            var links = LinkList.Parse(request);

            _logger.LogDebug($"Got {links.LinkCount} links.");

            return links;
        }

        public async Task<bool> SetProcessing(string listId)
        {
            var url = _urlBuilder.BuildSetProcessingUrl(listId);
            _logger.LogTrace($"setting {listId} to processing...");

            return await SendAsyncRequest(url);
        }

        public async Task<bool> SetState(string linkId, LinkState state)
        {
            var url = _urlBuilder.BuildSetStateUrl(
                linkId,
                Enum.GetName(typeof(LinkState), state)?.ToLower() ?? string.Empty);

            _logger.LogTrace($"setting {linkId} to state {state}...");

            return await SendAsyncRequest(url);
        }

        public async Task<bool> SetError(string linkId)
        {
            var url = _urlBuilder.BuildSetErrorUrl(linkId);
            _logger.LogTrace($"setting {linkId} to error...");
            return await SendAsyncRequest(url);
        }

        private async Task<bool> SendAsyncRequest(string url)
        {
            var request = await _httpservice.SendGetRequest(url);
            return request.Trim() == "OK";
        }
    }
}

