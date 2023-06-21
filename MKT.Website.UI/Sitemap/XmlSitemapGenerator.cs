using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
namespace MKT.Website.UI.Sitemap
{
    public interface IXmlSitemapGenerator
    {
        MemoryStream GenerateSitemap();
    }

    public class XmlSitemapOptions
    {
        public string BaseUrl { get; set; }
        public string ChangeFrequency { get; set; }
        public string Priority { get; set; }
    }

    public class XmlSitemapGenerator : IXmlSitemapGenerator
    {
        private readonly IWebHostEnvironment _environment;
        private readonly XmlSitemapOptions _options;

        public XmlSitemapGenerator(IWebHostEnvironment environment, IOptions<XmlSitemapOptions> options)
        {
            _environment = environment;
            _options = options.Value;
        }

        public MemoryStream GenerateSitemap()
        {
            var memoryStream = new MemoryStream();
            var xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true
            };

            using (var xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

                var urls = GetUrls(); // Implement your logic to retrieve the URLs of your website

                foreach (var url in urls)
                {
                    xmlWriter.WriteStartElement("url");

                    xmlWriter.WriteElementString("loc", _options.BaseUrl + url);
                    xmlWriter.WriteElementString("changefreq", _options.ChangeFrequency);
                    xmlWriter.WriteElementString("priority", _options.Priority);

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public List<string> GetUrls()
        {
            // Implement your logic to retrieve the URLs of your website
            // This is just a sample implementation

            var urls = new List<string>();

            // Retrieve URLs from your database, CMS, or any other source
            urls.Add("/"); // Home page
            urls.Add("/about");
            urls.Add("/products");
            urls.Add("/contact");

            return urls;
        }
    }
}
