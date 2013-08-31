using System;

namespace Pagansoft.Homeload.Core
{
    public class Api
    {
        private IHLTCHttpService _httpservice;
        private UrlBuilder _urlBuilder;

        public Api(IHLTCHttpService httpservice, UrlBuilder urlBuilder)
        {
            _httpservice = httpservice;
            _urlBuilder = urlBuilder;
        }

        public LinkList GetLinks()
        {
            var url = _urlBuilder.BuildGetLinksUrl();
            return LinkList.Parse(_httpservice.SendGetRequest(url));
        }

        public bool SetProcessing(string listId)
        {
            var url = _urlBuilder.BuildSetProcessingUrl(listId);
            return _httpservice.SendGetRequest(url) == "OK";
        }

        public bool SetState(string linkId, string listId, LinkState state)
        {
            var url = _urlBuilder.BuildSetStateUrl(
                linkId, 
                listId, 
                Enum.GetName(typeof(LinkState), state).ToLower());

            return _httpservice.SendGetRequest(url) == "OK";
        }
    }
}

