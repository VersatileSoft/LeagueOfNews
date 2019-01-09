using ExtensionMethods;
using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Web;

namespace Surrender_20.Core.Service
{
    public class NewsfeedService : INewsfeedService
    {
        private ObservableCollection<Newsfeed> NewsfeedCache { get; set; }
        private string LatestNewsfeedUrlCache { get; set; }
        private string NextPageUrl { get; set; }
        private Pages _parsingWay;
        private string _officialBaseURL;
        private ICookieWebClientService _cookieWebClientService;

        public NewsfeedService(ICookieWebClientService cookieWebClientService)
        {
            _cookieWebClientService = cookieWebClientService;
        }

        public async Task<ObservableCollection<Newsfeed>> LoadNewsfeedsAsync(string URL, Pages page)
        {
            _officialBaseURL = "https://" + new Uri(URL).Host;
            _parsingWay = page;
            if (LatestNewsfeedUrlCache != URL)
            {
                LatestNewsfeedUrlCache = URL;
                HtmlDocument doc = await _cookieWebClientService.GetPage(URL);
                switch (page)
                {
                    case Pages.SurrenderHome: NewsfeedCache = await LoadSurrender(doc); break;
                    case Pages.Official: NewsfeedCache = await LoadOfficial(doc); break;
                }
            }
            return NewsfeedCache;
        }

        public async Task<ObservableCollection<Newsfeed>> LoadMoreNewsfeeds()
        {
            ObservableCollection<Newsfeed> newsfeeds = new ObservableCollection<Newsfeed>();
            HtmlDocument doc = await _cookieWebClientService.GetPage(NextPageUrl);

            switch (_parsingWay)
            {                
                case Pages.SurrenderHome: newsfeeds = await LoadSurrender(doc); break;
                case Pages.Official: newsfeeds = await LoadOfficial(doc); break;
            }
            return newsfeeds;
        }

        public async Task<ObservableCollection<Newsfeed>> LoadOfficial(HtmlDocument Document)
        {
            ObservableCollection<Newsfeed> newsfeeds = new ObservableCollection<Newsfeed>();
            NextPageUrl = _officialBaseURL + Document.DocumentNode.SelectSingleNode("//a[@class='next']").Attributes["href"].Value;
            HtmlNodeCollection nodes = Document.DocumentNode.SelectNodes("//div[@class='gs-container']");

            foreach (HtmlNode node in nodes)
            {
                Newsfeed newsfeed = new Newsfeed();
                try
                {
                    newsfeed.Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").InnerText);
                    newsfeed.Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='horizontal-group']").InnerText);
                    newsfeed.UrlToNewsfeed = new Uri(_officialBaseURL + node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").Attributes["href"].Value);
                    newsfeed.Image = await _cookieWebClientService.GetImage(_officialBaseURL + node.SelectSingleNode(".//img").Attributes["src"].Value.ToString());
                    newsfeed.ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='teaser-content']").InnerText)
                        .RemoveSpaceFromString()
                        .RemoveContinueReadingString();
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

        public async Task<ObservableCollection<Newsfeed>> LoadSurrender(HtmlDocument Document)
        {
            ObservableCollection<Newsfeed> newsfeeds = new ObservableCollection<Newsfeed>();
            NextPageUrl = Document.DocumentNode.SelectSingleNode("//a[@class='nav-btm-right']").Attributes["href"].Value;
            HtmlNodeCollection nodes = Document.DocumentNode.SelectNodes("//div[@class='post-outer']");

            foreach (HtmlNode node in nodes)
            {

                Newsfeed newsfeed = new Newsfeed();

                try
                {
                    newsfeed.Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//h1[@class='news-title']").InnerText).RemoveSpaceFromString();
                    newsfeed.Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//span[@class='news-date']").InnerText).RemoveSpaceFromString();
                    newsfeed.UrlToNewsfeed = new Uri(node.SelectSingleNode(".//h1[@class='news-title']").SelectSingleNode(".//a").Attributes["href"].Value);
                    newsfeed.Image = await _cookieWebClientService.GetImage(node.SelectSingleNode(".//img").Attributes["src"].Value.ToString());
                    newsfeed.ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='news-content']").InnerText)
                        .RemoveSpaceFromString()
                        .RemoveContinueReadingString();
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
