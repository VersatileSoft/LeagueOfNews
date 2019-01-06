using ExtensionMethods;
using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
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

        public async Task<ObservableCollection<Newsfeed>> LoadNewsfeedsAsync(string URL, Pages page)
        {
            _parsingWay = page;
            if (LatestNewsfeedUrlCache != URL)
            {
                LatestNewsfeedUrlCache = URL;

                var client = new MyWebClient();
                HtmlDocument doc = client.GetPage(URL);

                //This request will be sent with the cookies obtained from the page
                doc = client.GetPage(URL);

                switch (page)
                {

                    case Pages.SurrenderHome: NewsfeedCache = LoadSurrender(doc); break;
                    case Pages.Official: NewsfeedCache = LoadOfficial(doc); break;

                }
            }

            return NewsfeedCache;
        }

        public ObservableCollection<Newsfeed> LoadOfficial(HtmlDocument Document)
        {
            var newsfeeds = new ObservableCollection<Newsfeed>();
            NextPageUrl = Document.DocumentNode.SelectSingleNode("//a[@class='next']").Attributes["href"].Value;
            var nodes = Document.DocumentNode.SelectNodes("//div[@class='gs-container']");

            foreach (HtmlNode node in nodes)
            {

                Newsfeed newsfeed = new Newsfeed();

                newsfeed.Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").InnerText);
                newsfeed.Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='horizontal-group']").InnerText);
                newsfeed.UrlToNewsfeed = new Uri(node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").Attributes["href"].Value);
                newsfeed.Image = "https://eune.leagueoflegends.com" + node.SelectSingleNode(".//img").Attributes["src"].Value.ToString();
                newsfeed.ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='teaser-content']").InnerText);



                newsfeeds.Add(newsfeed);

                if (newsfeed.Title == null || newsfeed.UrlToNewsfeed == null || newsfeed.Image == null || newsfeed.ShortDescription == null)
                {
                    throw new Exception();
                }
            }
            return newsfeeds;
        }

        public ObservableCollection<Newsfeed> LoadSurrender(HtmlDocument Document)
        {
            var newsfeeds = new ObservableCollection<Newsfeed>();
            NextPageUrl = Document.DocumentNode.SelectSingleNode("//a[@class='nav-btm-right']").Attributes["href"].Value;
            var nodes = Document.DocumentNode.SelectNodes("//div[@class='post-outer']");

            foreach (HtmlNode node in nodes)
            {

                Newsfeed newsfeed = new Newsfeed();

                try
                {
                    newsfeed.Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//h1[@class='news-title']").InnerText).RemoveSpaceFromString();
                    newsfeed.Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//span[@class='news-date']").InnerText).RemoveSpaceFromString();
                    newsfeed.UrlToNewsfeed = new Uri(node.SelectSingleNode(".//h1[@class='news-title']").SelectSingleNode(".//a").Attributes["href"].Value);
                    newsfeed.Image = node.SelectSingleNode(".//img").Attributes["src"].Value.ToString();
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

        public async Task<ObservableCollection<Newsfeed>> LoadMoreNewsfeeds()
        {

            ObservableCollection<Newsfeed> newsfeeds = new ObservableCollection<Newsfeed>();

            switch (_parsingWay)
            {

                case Pages.SurrenderHome:
                    var doc = await new HtmlWeb().LoadFromWebAsync(NextPageUrl);
                    newsfeeds = LoadSurrender(doc);
                    break;
                case Pages.Official:

                    var client = new MyWebClient();
                    HtmlDocument doc1 = client.GetPage("https://eune.leagueoflegends.com" + NextPageUrl);
                    doc = client.GetPage("https://eune.leagueoflegends.com" + NextPageUrl);
                    newsfeeds = LoadOfficial(doc1);
                    break;

            }

            return newsfeeds;
        }
    }

    public class MyWebClient
    {
        //The cookies will be here.
        private CookieContainer _cookies = new CookieContainer();

        //In case you need to clear the cookies
        public void ClearCookies()
        {
            _cookies = new CookieContainer();
        }

        public HtmlDocument GetPage(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";

            IWebProxy prox = request.Proxy;

            prox.Credentials = CredentialCache.DefaultCredentials;

            //This is the important part.
            request.CookieContainer = _cookies;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();

            //When you get the response from the website, the cookies will be stored
            //automatically in "_cookies".

            using (var reader = new StreamReader(stream))
            {
                string html = reader.ReadToEnd();
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                return doc;
            }
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
