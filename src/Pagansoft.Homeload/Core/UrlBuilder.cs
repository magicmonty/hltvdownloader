using System;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.Composition;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IUrlBuilder))]
    public class UrlBuilder : IUrlBuilder
    {
        private const string BaseUrl = "http://www.homeloadtv.com/api/?do={0}&uid={1}&password={2}";
        private string _username;
        private string _password;

        [ImportingConstructor]
        public UrlBuilder(IConfiguration configuration)
        {
            _username = configuration.HltvUserName;
            var md5 = MD5.Create();
            _password = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(configuration.HltvPassword)))
                                    .Replace("-", "")
                                    .Trim()
                                    .ToUpper();
        }

        public string BuildGetLinksUrl()
        {
            return BuildGetLinksUrl(processingToNew: false);
        }

        public string BuildGetLinksUrl(bool processingToNew)
        {
            var procToNew = processingToNew ? "&proctonew=true" : string.Empty;

            return string.Format(BaseUrl, "getlinks", _username, _password) + procToNew;
        }

        public string BuildSetProcessingUrl(string listId)
        {
            return string.Format(BaseUrl, "setstate", _username, _password) 
                + string.Format("&list={0}&state=processing", listId);
        }

        public string BuildSetStateUrl(string linkId, string listId, string state)
        {
            var result = string.Format(BaseUrl, "setstate", _username, _password) 
                + string.Format("&id={0}&list={1}&state={2}", linkId, listId, state);
            if (state == "finished")
                result += "&error=";

            return result;
        }

        public string BuildSetErrorUrl(string linkId, string listId)
        {
            return BuildSetStateUrl(linkId, listId, "finished") + "brokenonopen";
        }
    }
}

