﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Seventy.Web.StartupCustomizations
{
    public class SeoTools
    {
        public enum ChangeFrequency
        {
            Always,
            Hourly,
            Daily,
            Weekly,
            Monthly,
            Yearly,
            Never
        }
        public class SitemapUrl
        {
            public string Url { get; set; }
            public DateTime? Modified { get; set; }
            public ChangeFrequency? ChangeFrequency { get; set; }
            public double? Priority { get; set; }
        }
        public class SitemapBuilder
        {
            private readonly XNamespace _ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            private List<SitemapUrl> _urls;

            public SitemapBuilder()
            {
                _urls = new List<SitemapUrl>();
            }

            public void AddUrl(string url, DateTime? modified = null, ChangeFrequency? changeFrequency = null, double? priority = null)
            {
                _urls.Add(new SitemapUrl()
                {
                    Url = url,
                    Modified = modified,
                    ChangeFrequency = changeFrequency,
                    Priority = priority,
                });
            }

            public override string ToString()
            {
                var sitemap = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement(_ns + "urlset",
                        from item in _urls
                        select CreateItemElement(item)
                        ));

                return sitemap.ToString();
            }

            private XElement CreateItemElement(SitemapUrl url)
            {
                XElement itemElement = new XElement(_ns + "url", new XElement(_ns + "loc", url.Url.ToLower()));

                if (url.Modified.HasValue)
                {
                    itemElement.Add(new XElement(_ns + "lastmod", url.Modified.Value.ToString("yyyy-MM-ddTHH:mm:ss.f") + "+00:00"));
                }

                if (url.ChangeFrequency.HasValue)
                {
                    itemElement.Add(new XElement(_ns + "changefreq", url.ChangeFrequency.Value.ToString().ToLower()));
                }

                if (url.Priority.HasValue)
                {
                    itemElement.Add(new XElement(_ns + "priority", url.Priority.Value.ToString("N1")));
                }

                return itemElement;
            }
        }

        }
}
