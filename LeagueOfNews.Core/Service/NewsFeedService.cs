using ExtensionMethods;
using HtmlAgilityPack;
using LeagueOfNews.Core.Interface;
using LeagueOfNews.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace LeagueOfNews.Core.Service
{
    public class NewsfeedService : INewsfeedService
    {
        private string _baseURL;

        private IList<Newsfeed> _newsfeeds { get; set; }
        private readonly Dictionary<NewsCategory, string> _nextPageUrls;

        private readonly IWebClientService _cookieWebClientService;
        private readonly ISettingsService _settingsService;
        private readonly IOperatingSystemService _operatingSystemService;

        public NewsfeedService(IWebClientService cookieWebClientService, ISettingsService settingsService, IOperatingSystemService operatingSystemService)
        {
            _cookieWebClientService = cookieWebClientService;
            _settingsService = settingsService;
            _operatingSystemService = operatingSystemService;
            _nextPageUrls = new Dictionary<NewsCategory, string>();
        }

        public async Task<IList<Newsfeed>> LoadNewsfeedsAsync(NewsCategory page)
        {
            string URL = _settingsService[page].CategoryUrl;
            NewsWebsite newsWebsite = _settingsService[page].Website;
            _baseURL = "https://" + new Uri(URL).Host;

            HtmlDocument doc = await _cookieWebClientService.GetPage(URL, page);

            if (doc == null)
            {
                return new List<Newsfeed>();
            }

            switch (newsWebsite)
            {
                case NewsWebsite.LoL:
                    _newsfeeds = await LoadOfficial(doc, page);
                    break;
                case NewsWebsite.DevCorner:
                    _newsfeeds = LoadDevCorner(doc, page);
                    break;
                case NewsWebsite.Surrender:
                    _newsfeeds = await LoadSurrender(doc, page);
                    break;
            }

            return _newsfeeds;
        }

        public async Task<IList<Newsfeed>> LoadMoreNewsfeeds(NewsCategory page)
        {
            List<Newsfeed> newsfeeds = new List<Newsfeed>();
            HtmlDocument doc;
            if (_nextPageUrls.TryGetValue(page, out string url))
            {
                doc = await _cookieWebClientService.GetPage(url, page);
            }
            else
            {
                throw new Exception("Filed to load next page url");
            }

            switch (_settingsService[page].Website)
            {
                case NewsWebsite.LoL:
                    newsfeeds = await LoadOfficial(doc, page);
                    break;
                case NewsWebsite.DevCorner:
                    newsfeeds = LoadDevCorner(doc, page);
                    break;
                case NewsWebsite.Surrender:
                    newsfeeds = await LoadSurrender(doc, page);
                    break;
            }
            return newsfeeds;
        }

        public async Task<List<Newsfeed>> LoadOfficial(HtmlDocument Document, NewsCategory page)
        {
            List<Newsfeed> newsfeeds = new List<Newsfeed>();
            _nextPageUrls[page] = _baseURL + Document.DocumentNode.SelectSingleNode("//a[@class='next']").Attributes["href"].Value;
            HtmlNodeCollection nodes = Document.DocumentNode.SelectNodes("//div[@class='gs-container']");

            foreach (HtmlNode node in nodes)
            {
                try
                {
                    Newsfeed newsfeed = new Newsfeed
                    {
                        Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").InnerText).RemoveSpaceFromString(),
                        Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='horizontal-group']").InnerText),
                        UrlToNewsfeed = _baseURL + node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").Attributes["href"].Value,
                        Image = await _cookieWebClientService.GetImageAsync(_baseURL + node.SelectSingleNode(".//img").Attributes["src"].Value),
                        ImageUri = _baseURL + node.SelectSingleNode(".//img").Attributes["src"].Value,
                        ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='teaser-content']").InnerText).RemoveSpaceFromString(),
                        Page = page
                        //TODO newsfeed.Website
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

        public List<Newsfeed> LoadDevCorner(HtmlDocument Document, NewsCategory page)
        {
            List<Newsfeed> newsfeeds = new List<Newsfeed>();
            HtmlNodeCollection nodes = Document.DocumentNode.SelectNodes("//body/div[@class='content']/div/div/div/div/table/tbody[@id='discussion-list']/tr");

            foreach (HtmlNode node in nodes)
            {
                try
                {
                    Newsfeed newsfeed = new Newsfeed
                    {
                        Title = HttpUtility.HtmlDecode(node?.SelectSingleNode(".//td[@class='title']/div/a/span").InnerText),
                        Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//td[@class='title']/div[@class='discussion-footer byline opaque']").InnerText).RemoveSpaceFromString() + " " +
                        HttpUtility.HtmlDecode(node.SelectSingleNode(".//td[@class='title']/div[@class='discussion-footer byline opaque']/span").InnerText),
                        UrlToNewsfeed = _baseURL + node.SelectSingleNode(".//td[@class='title']/div/a").Attributes["href"].Value,
                        ShortDescription = HttpUtility.HtmlDecode(node?.SelectSingleNode(".//td[@class='title']/div/a/span").Attributes["title"].Value.RemoveSpaceFromString()),
                        Page = page
                        //TODO newsfeed.Website
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

        public async Task<List<Newsfeed>> LoadSurrender(HtmlDocument Document, NewsCategory page)
        {
            List<Newsfeed> newsfeeds = new List<Newsfeed>();
            _nextPageUrls[page] = Document.DocumentNode.SelectSingleNode("//a[@class='nav-btm-right']").Attributes["href"].Value;
            HtmlNodeCollection nodes = Document.DocumentNode.SelectNodes("//div[@class='post-outer']");

            foreach (HtmlNode node in nodes)
            {
                try
                {
                    Newsfeed newsfeed = new Newsfeed
                    {
                        Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//h1[@class='news-title']").InnerText).RemoveSpaceFromString(),
                        Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//span[@class='news-date']").InnerText).RemoveSpaceFromString(),
                        UrlToNewsfeed = node.SelectSingleNode(".//h1[@class='news-title']").SelectSingleNode(".//a").Attributes["href"].Value + "?m=1",
                        Image = await _cookieWebClientService.GetImageAsync(node.SelectSingleNode(".//img").Attributes["src"].Value),
                        ImageUri = node.SelectSingleNode(".//img").Attributes["src"].Value,
                        ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='news-content']").InnerText).RemoveSpaceFromString().RemoveContinueReadingString(),
                        Page = page
                        //TODO newsfeed.Website
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

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static string RemoveSpaceFromString(this string s)
        {
            return s.Replace("\n", " ")
                    .Replace("\n\n", " ")
                    .Replace(Environment.NewLine, " ")
                    .Replace("          ", " ")
                    .Replace("   ", " ")
                    .Replace("  ", " ")
                    .Replace(" ------------------------------------------------------------------------------- **Usual Disclaimers**", " ")
                    .Trim();
        }

        public static string RemoveContinueReadingString(this string s)
        {
            return s.Replace(" Bundle up and continue reading for more information!", "")
                    .Replace(" Continue for more information and previews!", "")
                    .Replace(" Continue reading for more information!", "")
                    .Replace(" Continue reading for these champions' regular store prices.", "")
                    .Replace(" Continue reading for a better look at this sale's discounted skins!", "")
                    .Replace(" Continue reading for more details!", "")
                    .Replace(" Continue reading for a spoiler free look at this week's games, including team info, schedules, and VODs!", "")
                    .Replace(" Continue reading for a full preview of the skin!", "")
                    .Replace(" Continue reading for a look at the article and new Universe content!", "")
                    .Replace(" Continue reading for more info on the video!", "");
        }
    }
}
