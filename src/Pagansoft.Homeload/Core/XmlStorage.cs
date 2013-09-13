using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Pagansoft.Homeload.Core
{
    [Export(typeof(IStorage))]
    public class XmlStorage : IStorage
    {
        IConfiguration _configuration;
        string _fileName;

        [ImportingConstructor]
        public XmlStorage(IConfiguration configuration)
        {
            _configuration = configuration;
            _fileName = Path.Combine(_configuration.ConfigurationDirectory, "session.xml");
        }

        public IEnumerable<LinkIdPersistenceModel> LoadLinks()
        {
            var result = new List<LinkIdPersistenceModel>();
            if (!File.Exists(_fileName))
                return result;

            try
            {
                XDocument doc = XDocument.Load(_fileName);

                var root = doc.Root;
                if (root == null || root.Name != "links")
                {
                    File.Delete(_fileName);
                    return result;
                }

                foreach (var link in root.Elements().Where(e => e.Name == "link"))
                {
                    var gid = GetAttributeValue(link, "gid");
                    if (string.IsNullOrEmpty(gid))
                        continue;

                    var listId = GetAttributeValue(link, "listId");
                    if (string.IsNullOrEmpty(listId))
                        continue;
                    
                    var linkId = GetAttributeValue(link, "linkId");
                    if (string.IsNullOrEmpty(linkId))
                        continue;
                    
                    var url = link.Value;
                    if (string.IsNullOrEmpty(url))
                        continue;

                    result.Add(new LinkIdPersistenceModel(listId, linkId, url, gid));
                }
            }
            catch
            {
            }

            return result;
        }

        public static string GetAttributeValue(XElement element, string name)
        {
            var attr = element.Attribute(name) ?? new XAttribute(name, "");
            return attr.Value ?? string.Empty;
        }

        public void SaveLinks(IEnumerable<LinkIdPersistenceModel> links)
        {
            var doc = new XDocument();
            var root = new XElement("links");

            links.ToList()
                 .ForEach(item => {
                root.Add(new XElement("link", 
                                      new XAttribute("linkId", item.LinkId),
                                      new XAttribute("listId", item.ListId),
                                      new XAttribute("gid", item.Gid),
                                      item.Url));
            });

            doc.Add(root);
            doc.Save(_fileName);
        }
    }
}

