using Ducksoft.NetCore.Razor.Sitemap.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MKT.Website.UI.Controllers
{
    public class SitemapController : Controller
    {
        public IActionResult Index()
        {
            var sitemapNodes = new List<SitemapNode>
            {
                new SitemapNode(DateTime.UtcNow, 0.6400, "daily", Url.Action("Index", "Home")),
                new SitemapNode(DateTime.UtcNow, 0.6400, "daily", Url.Action("About", "Home")),
                new SitemapNode(DateTime.UtcNow, 0.6400, "daily", Url.Action("ContactUs", "Home")),
                new SitemapNode(DateTime.UtcNow, 0.6400, "daily", Url.Action("Index", "Person")),
                new SitemapNode(DateTime.UtcNow, 0.6400, "daily", Url.Action("WebDevelopment", "Service")),
                new SitemapNode(DateTime.UtcNow, 0.6400, "daily", Url.Action("Index", "Company")),
                // Add more URLs to your sitemap here
            };

            return new SitemapResult(sitemapNodes);
            //return View("Sitemap");
        }

        public class SitemapNode
        {
            public string Url { get; set; }
            public DateTime LastModified { get; set; } 
            public double Priority { get; set; }
            public String Frequency { get; set; }


            public SitemapNode(DateTime datetime, double priority, string frequency, string url)
            {
                Url = url;
                LastModified = datetime;
                Priority = priority;
                Frequency = frequency;
            }
        }

        public class SitemapResult : ActionResult
        {
            private readonly IEnumerable<SitemapNode> _sitemapNodes;

            public SitemapResult(IEnumerable<SitemapNode> sitemapNodes)
            {
                _sitemapNodes = sitemapNodes;
            }

            public override void ExecuteResult(ActionContext context)
            {
                context.HttpContext.Response.ContentType = "application/xml";

                using (var writer = new System.IO.StreamWriter(context.HttpContext.Response.Body))
                {
                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                    writer.WriteLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

                    foreach (var node in _sitemapNodes)
                    {
                        writer.WriteLine("<url>");
                        writer.WriteLine($"<loc>{node.Url}</loc>");
                        writer.WriteLine($"<lastmod>{node.LastModified}</lastmod>");
                        writer.WriteLine($"<changefreq>{node.Frequency}</changefreq>");
                        writer.WriteLine($"<priority>{node.Priority}</priority>");
                        writer.WriteLine("</url>");
                    }

                    writer.WriteLine("</urlset>");
                }
            }
        }
    }
}
