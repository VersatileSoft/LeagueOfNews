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


        public async Task<ObservableCollection<Newsfeed>> LoadNewsfeedsAsync(string URL)
        {

            if (LatestNewsfeedUrlCache != URL)
            {
                LatestNewsfeedUrlCache = URL;
                var doc = await new HtmlWeb().LoadFromWebAsync(URL);
                NewsfeedCache = Load(doc);
            }

            return NewsfeedCache;
        }

        public ObservableCollection<Newsfeed> Load(HtmlDocument Document)
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

                //newsfeeds.Add(new Newsfeed()
                //{
                //    Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//h1[@class='news-title']").InnerText).RemoveEnterFromString(),
                //    UrlToNewsfeed = new Uri(node.SelectSingleNode(".//h1[@class='news-title']").SelectSingleNode(".//a").Attributes["href"].Value),
                //    Image = node.SelectSingleNode(".//img").Attributes["src"].Value.ToString(),
                //    ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='news-content']").InnerText).RemoveEnterFromString()
                //});


            }
            return newsfeeds;
        }

        public async Task<ObservableCollection<Newsfeed>> LoadMoreNewsfeeds()
        {
            var doc = await new HtmlWeb().LoadFromWebAsync(NextPageUrl);
            return Load(doc);
        }
    }
}

namespace ExtensionMethods
{
    public static class StringExtensions //weź kurwa przydaj sie do czegoś Woźniak i zrób z tym porządek
                                         //bo jest aktualnie 5:09 i nie myśle a kodowa stonoga mi sie zrobiła wyżej
                                         //~Twój najlepszy kolega Kacper <3
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
            return s.Replace(" Continue reading for more information!", "")
                    .Replace(" Continue for more information and previews!", "")
                    .Replace(" Continue reading for these champions' regular store prices.", "")
                    .Replace(" Continue reading for a better look at this sale's discounted skins!", "")
                    .Replace(" Continue reading for more details!", "")
                    .Replace(" Continue reading for a spoiler free look at this week's games, including team info, schedules, and VODs!", "")
                    .Replace(" Continue reading for more info on the video!", "");
        }
    }
}
