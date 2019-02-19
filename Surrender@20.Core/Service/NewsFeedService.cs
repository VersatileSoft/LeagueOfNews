using ExtensionMethods;
using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Surrender_20.Core.Service
{
    public class NewsfeedService : INewsfeedService
    {
        private string _officialBaseURL;

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
            string URL = _settingsService[page].CategoryURL;
            _officialBaseURL = "https://" + new Uri(URL).Host;

            HtmlDocument doc = await _cookieWebClientService.GetPage(URL, page);

            if (doc == null)
            {
                return new List<Newsfeed>();
            }

            switch (page)
            {
                case NewsCategory.SurrenderHome:
                case NewsCategory.ESports:
                case NewsCategory.PBE:
                case NewsCategory.RedPosts:
                case NewsCategory.Rotations:
                case NewsCategory.Releases: _newsfeeds = await LoadSurrender(doc, page); break;
                case NewsCategory.Official: _newsfeeds = await LoadOfficial(doc, page); break;
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

            switch (page)
            {
                case NewsCategory.SurrenderHome:
                case NewsCategory.ESports:
                case NewsCategory.PBE:
                case NewsCategory.RedPosts:
                case NewsCategory.Rotations:
                case NewsCategory.Releases: newsfeeds = await LoadSurrender(doc, page); break;
                case NewsCategory.Official: newsfeeds = await LoadOfficial(doc, page); break;
            }
            return newsfeeds;
        }

        public async Task<List<Newsfeed>> LoadOfficial(HtmlDocument Document, NewsCategory page)
        {
            List<Newsfeed> newsfeeds = new List<Newsfeed>();
            _nextPageUrls[page] = _officialBaseURL + Document.DocumentNode.SelectSingleNode("//a[@class='next']").Attributes["href"].Value;
            HtmlNodeCollection nodes = Document.DocumentNode.SelectNodes("//div[@class='gs-container']");

            foreach (HtmlNode node in nodes)
            {
                Newsfeed newsfeed = new Newsfeed();
                try
                {
                    newsfeed.Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").InnerText);
                    newsfeed.Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='horizontal-group']").InnerText);
                    newsfeed.UrlToNewsfeed = _officialBaseURL + node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").Attributes["href"].Value;
                    newsfeed.Image = await _cookieWebClientService.GetImage(_officialBaseURL + node.SelectSingleNode(".//img").Attributes["src"].Value.ToString());
                    newsfeed.ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='teaser-content']").InnerText)
                        .RemoveSpaceFromString();
                    newsfeed.Page = page;
                }
                catch { continue; }

                newsfeeds.Add(newsfeed);

                if (newsfeed.Title == null || newsfeed.UrlToNewsfeed == null || newsfeed.Image == null || newsfeed.ShortDescription == null)
                {
                    throw new Exception();
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
                Newsfeed newsfeed = new Newsfeed();
                try
                {
                    newsfeed.Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//h1[@class='news-title']").InnerText).RemoveSpaceFromString();
                    newsfeed.Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//span[@class='news-date']").InnerText).RemoveSpaceFromString();
                    if (_operatingSystemService.GetSystemType() == SystemType.Android)
                    {
                        newsfeed.UrlToNewsfeed = node.SelectSingleNode(".//h1[@class='news-title']").SelectSingleNode(".//a").Attributes["href"].Value + "?m=1";
                    }
                    else if (_operatingSystemService.GetSystemType() == SystemType.UWP)
                    {
                        newsfeed.UrlToNewsfeed = node.SelectSingleNode(".//h1[@class='news-title']").SelectSingleNode(".//a").Attributes["href"].Value;
                        newsfeed.Image = await _cookieWebClientService.GetImage(node.SelectSingleNode(".//img").Attributes["src"].Value.ToString());
                    }

                    newsfeed.ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='news-content']").InnerText)
                        .RemoveSpaceFromString()
                        .RemoveContinueReadingString();
                    newsfeed.Page = page;
                }
                catch { continue; }

                newsfeeds.Add(newsfeed);

                //if (newsfeed.Title == null || newsfeed.UrlToNewsfeed == null || newsfeed.Image == null || newsfeed.ShortDescription == null)
                //{
                //    throw new Exception();
                //}
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
                    .Replace("          ", " ")
                    .Replace("   ", " ")
                    .Replace("  ", " ")
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