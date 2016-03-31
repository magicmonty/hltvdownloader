using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Threading;

namespace Pagansoft.Homeload.Core
{
    internal static class XmlHelpers
    {
        internal static string AttributeValue(this XElement element, XName attributeName)
        {
            if (element == null)
                return string.Empty;

            return element
                .Attributes(attributeName)
                .Select(a => a.Value)
                .FirstOrDefault() ?? string.Empty;
        }
    }

    [Export(typeof(IStorage))]
    public class XmlStorage : IStorage
    {
        private readonly string _fileName;
        private readonly string _lockFileName;

        [ImportingConstructor]
        public XmlStorage(IConfiguration configuration)
        {
            _fileName = Path.Combine(configuration.ConfigurationDirectory, "session.xml");
            _lockFileName = _fileName + ".lock";
        }

        public void Lock()
        {
            try
            {
                while (File.Exists(_lockFileName))
                    Thread.Yield();

                File.Create(_lockFileName);
            }
            catch
            {
                /* intentionally left blank */
            }
        }

        public void Release()
        {
            if (!File.Exists(_lockFileName))
                return;

            try
            {
                File.Delete(_lockFileName);
            }
            catch
            {
                /* intentionally left blank */
            }
        }

        public IEnumerable<LinkIdPersistenceModel> LoadLinks()
        {
            if (!File.Exists(_fileName))
                return Enumerable.Empty<LinkIdPersistenceModel>();

            try
            {
                var doc = XDocument.Load(_fileName);

                var root = doc.Root;
                if (root != null && root.Name == "links")
                    return root.Elements("link").Select(FromXml).ToList();

                File.Delete(_fileName);
                return Enumerable.Empty<LinkIdPersistenceModel>();
            }
            catch
            {
                return Enumerable.Empty<LinkIdPersistenceModel>();
            }
        }

        private static LinkIdPersistenceModel FromXml(XElement link)
        {
            var gid = link.AttributeValue("gid");
            var listId = link.AttributeValue("listId");
            var linkId = link.AttributeValue("linkId");
            var url = link.Value;

            return new[] { gid, listId, linkId, url }.Any(string.IsNullOrEmpty)
                ? null
                : new LinkIdPersistenceModel(listId, linkId, url, gid);
        }

        public void SaveLinks(IEnumerable<LinkIdPersistenceModel> links)
        {
            var doc = new XDocument();
            var root = new XElement("links");

            links.ToList()
                 .ForEach(item =>
                    root.Add(
                        new XElement(
                            "link",
                            new XAttribute("linkId", item.LinkId),
                            new XAttribute("listId", item.ListId),
                            new XAttribute("gid", item.Gid),
                            item.Url)));

            doc.Add(root);
            doc.Save(_fileName);
        }
    }
}

