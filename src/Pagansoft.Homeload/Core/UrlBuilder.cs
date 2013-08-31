using System;
using System.Security.Cryptography;
using System.Text;

namespace Pagansoft.Homeload.Core
{
    public class UrlBuilder
    {
        private const string BaseUrl = "http://www.homeloadtv.com/api/?do={0}&uid={1}&password={2}";
        private string _username;
        private string _password;

        public UrlBuilder(string username, string password)
        {
            _username = username;
            var md5 = MD5.Create();
            _password = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)))
                                    .Replace("-", "")
                                    .Trim()
                                    .ToUpper();
        }

        public string BuildGetLinksUrl()
        {
            return string.Format(BaseUrl, "getlinks", _username, _password);
        }

        public string BuildSetProcessingUrl(string listId)
        {
            return string.Format(BaseUrl, "setstate", _username, _password) 
                + string.Format("&list={0}&state=processing", listId);
        }

        public string BuildSetStateUrl(string linkId, string listId, string state)
        {
            return string.Format(BaseUrl, "setstate", _username, _password) 
                + string.Format("&id={0}&list={1}&state={2}", linkId, listId, state);
        }
    }
}

