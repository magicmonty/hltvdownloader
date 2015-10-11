using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IHltvApi))]
    public class Api : IHltvApi
    {
        IHLTCHttpService _httpservice;
        IUrlBuilder _urlBuilder;

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
            var url = _urlBuilder.BuildGetLinksUrl(initial);

            var request = await _httpservice.SendGetRequest(url);

            return LinkList.Parse(request);
        }

        public async Task<bool> SetProcessing(string listId)
        {
            var url = _urlBuilder.BuildSetProcessingUrl(listId);

            return await SendAsyncRequest(url);
        }

        public async Task<bool> SetState(string linkId, LinkState state)
        {
            var url = _urlBuilder.BuildSetStateUrl(
                linkId, 
                Enum.GetName(typeof(LinkState), state).ToLower());

            return await SendAsyncRequest(url);
        }

        public async Task<bool> SetError(string linkId)
        {
            var url = _urlBuilder.BuildSetErrorUrl(linkId);
            return await SendAsyncRequest(url);
        }

        async Task<bool> SendAsyncRequest(string url)
        {
            var request = await _httpservice.SendGetRequest(url);
            return request.Trim() == "OK";
        }
    }
}

