using System;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IHltvApi))]
    public class Api : IHltvApi
    {
        private IHLTCHttpService _httpservice;
        private IUrlBuilder _urlBuilder;

        [ImportingConstructor]
        public Api(IHLTCHttpService httpservice, IUrlBuilder urlBuilder)
        {
            _httpservice = httpservice;
            _urlBuilder = urlBuilder;
        }

        public Task<LinkList> GetLinks()
        {
            return GetLinks(initial: false);
        }

        public Task<LinkList> GetLinks(bool initial)
        {
            var url = _urlBuilder.BuildGetLinksUrl(initial);

            var task = Task.Factory.StartNew<string>(() => _httpservice.SendGetRequest(url))
                                   .ContinueWith<LinkList>(request => LinkList.Parse(request.Result));

            return task;
        }

        public Task<bool> SetProcessing(string listId)
        {
            var url = _urlBuilder.BuildSetProcessingUrl(listId);

            return SendAsyncRequest(url);
        }

        public Task<bool> SetState(string linkId, string listId, LinkState state)
        {
            var url = _urlBuilder.BuildSetStateUrl(
                linkId, 
                listId, 
                Enum.GetName(typeof(LinkState), state).ToLower());

            return SendAsyncRequest(url);
        }

        public Task<bool> SetError(string linkId, string listId)
        {
            var url = _urlBuilder.BuildSetErrorUrl(
                linkId, 
                listId);

            return SendAsyncRequest(url);
        }

        private Task<bool> SendAsyncRequest(string url)
        {
            var task = Task.Factory.StartNew<string>(() => _httpservice.SendGetRequest(url))
                .ContinueWith<bool>(request => request.Result == "OK");

            return task;
        }
    }
}

