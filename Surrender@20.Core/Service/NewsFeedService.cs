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
            NextPageUrl = Document.DocumentNode.SelectSingleNode("//a[@class='blog-pager-older-link']").Attributes["href"].Value;
            var nodes = Document.DocumentNode.SelectNodes("//div[@class='mobile-post-outer']");
            foreach (HtmlNode node in nodes)
            {
                newsfeeds.Add(new Newsfeed()
                {
                    Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//h3[@class='mobile-index-title entry-title']").InnerText),
                    UrlToNewsfeed = new Uri(node.SelectSingleNode(".//a").Attributes["href"].Value),
                    Image = node.SelectSingleNode(".//img").Attributes["src"].Value.ToString(),
                    ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='post-body']").InnerText)
                });
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
