using System;
using System.ComponentModel.Composition;
using System.Security.Cryptography;
using System.Text;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IUrlBuilder))]
    public class UrlBuilder : IUrlBuilder
    {
        private const string BaseUrl = "http://www.homeloadtv.com/api/?do={0}&uid={1}&password={2}";
        private readonly string _username;
        private readonly string _password;

        [ImportingConstructor]
        public UrlBuilder(IConfiguration configuration)
        {
            _username = configuration.HltvUserName;
            var md5 = MD5.Create();
            _password = BitConverter
                .ToString(md5.ComputeHash(Encoding.Default.GetBytes(configuration.HltvPassword)))
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
                + $"&list={listId}&state=processing";
        }

        public string BuildSetStateUrl(string linkId, string state)
        {
            var result = string.Format(BaseUrl, "setstate", _username, _password)
                + $"&id={linkId}&state={state}";

            if (state == "finished" || state == "error")
                result += "&error=";

            return result;
        }

        public string BuildSetErrorUrl(string linkId)
        {
            return BuildSetStateUrl(linkId, "error") + "brokenonopen";
        }
    }
}

