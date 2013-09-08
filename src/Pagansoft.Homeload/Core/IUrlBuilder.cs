using System;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.Composition;

namespace Pagansoft.Homeload.Core
{
    public interface IUrlBuilder
    {
        string BuildGetLinksUrl();

        string BuildGetLinksUrl(bool processingToNew);

        string BuildSetProcessingUrl(string listId);

        string BuildSetStateUrl(string linkId, string state);

        string BuildSetErrorUrl(string linkId);
    }
}
