using HtmlAgilityPack;
using LeagueOfNews.Model;
using LeagueOfNews.WebApi.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LeagueOfNews.WebApi.Services
{
    public class NewsfeedService : INewsfeedService
    {
        private readonly AppConfig _appConfig;
        public NewsfeedService(IOptions<AppConfig> appConfig)
        {
            _appConfig = appConfig.Value;
        }

        public async Task<IEnumerable<Newsfeed>> GetNewsfeeds(int websiteId, int page)
        {
            string websiteUrl = _appConfig.Websites
                .Concat(_appConfig.Websites.Where(w => w.Subpages != null).SelectMany(w => w.Subpages).AsEnumerable())
                .Where(w => w.Id == websiteId)
                .Select(w => w.Url)
                .FirstOrDefault();

            string baseUrl = "https://" + new Uri(websiteUrl).Host;
            HtmlDocument document = await new HtmlWeb().LoadFromWebAsync(websiteUrl);

            return websiteId switch
            {
                0 => ParseOfficial(document, baseUrl),
                var x when x >= 1 && x <= 6 => ParseSurrender(document, baseUrl),
                7 => ParseDev(document, baseUrl),
                _ => null,
            };
        }

        public IEnumerable<Newsfeed> ParseOfficial(HtmlDocument document, string baseUrl)
        {
            List<Newsfeed> newsfeeds = new List<Newsfeed>();
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//div[@class='gs-container']");

            foreach (HtmlNode node in nodes)
            {
                try
                {
                    Newsfeed newsfeed = new Newsfeed
                    {
                        Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").InnerText).Replace("\n", ""),
                        Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='horizontal-group']").InnerText).Replace("\n", ""),
                        UrlToNewsfeed = baseUrl + node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").Attributes["href"].Value,
                        ImageUrl = baseUrl + node.SelectSingleNode(".//img").Attributes["src"].Value,
                        ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='teaser-content']").InnerText).Replace("\n", ""),
                    };
                    newsfeeds.Add(newsfeed);
                }
                catch
                {
                    continue;
                }
            }
            return newsfeeds;
        }

        public IEnumerable<Newsfeed> ParseSurrender(HtmlDocument Document, string baseUrl)
        {
            List<Newsfeed> newsfeeds = new List<Newsfeed>();
            HtmlNodeCollection nodes = Document.DocumentNode.SelectNodes("//div[@class='post-outer']");

            foreach (HtmlNode node in nodes)
            {
                try
                {
                    Newsfeed newsfeed = new Newsfeed
                    {
                        Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//h1[@class='news-title']").InnerText).Replace("\n", ""),
                        Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//span[@class='news-date']").InnerText).Replace("\n", ""),
                        UrlToNewsfeed = node.SelectSingleNode(".//h1[@class='news-title']").SelectSingleNode(".//a").Attributes["href"].Value + "?m=1",
                        ImageUrl = node.SelectSingleNode(".//img").Attributes["src"].Value,
                        ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='news-content']").InnerText).Replace("\n", ""),
                    };
                    newsfeeds.Add(newsfeed);
                }
                catch
                {
                    continue;
                }
            }
            return newsfeeds;
        }

        public List<Newsfeed> ParseDev(HtmlDocument Document, string baseUrl)
        {
            List<Newsfeed> newsfeeds = new List<Newsfeed>();
            HtmlNodeCollection nodes = Document.DocumentNode.SelectNodes("//body/div[@class='content']/div/div/div/div/table/tbody[@id='discussion-list']/tr");

            foreach (HtmlNode node in nodes)
            {
                try
                {
                    Newsfeed newsfeed = new Newsfeed
                    {
                        Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//td[@class='title']/div/a/span").InnerText.Replace("\n", "")),
                        Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//td[@class='title']/div[@class='discussion-footer byline opaque']").InnerText + " " +
                        node.SelectSingleNode(".//td[@class='title']/div[@class='discussion-footer byline opaque']/span").InnerText).Replace("\n", ""),
                        UrlToNewsfeed = baseUrl + node.SelectSingleNode(".//td[@class='title']/div/a").Attributes["href"].Value,
                        ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//td[@class='title']/div/a/span").Attributes["title"].Value).Replace("\n", ""),
                    };

                    newsfeeds.Add(newsfeed);
                }
                catch
                {
                    continue;
                }
            }
            return newsfeeds;
        }
    }
}
