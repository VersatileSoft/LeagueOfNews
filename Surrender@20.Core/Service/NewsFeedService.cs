using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Surrender_20.Core.Service
{
    public class NewsfeedService : INewsfeedService
    {
        private ObservableCollection<Newsfeed> NewsfeedCache { get; set; }
        private string LatestNewsfeedUrlCache { get; set; }

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
            var nodes = Document.DocumentNode.SelectNodes("//div[@class='mobile-post-outer']");
            foreach (HtmlNode node in nodes)
            {
                //newsfeeds.Add(new Newsfeed()
                //{
                var Title = HttpUtility.HtmlDecode(node.SelectSingleNode("./h3[@class='mobile-index-title entry-title']").InnerText);
                var UrlToNewsfeed = new Uri(node.SelectSingleNode("./a").Attributes["href"].Value);
                var Image = node.SelectSingleNode(".//img").Attributes["src"].Value.ToString();
                var ShortDescription = node.SelectSingleNode("./div[@class='post-body']").InnerText;
               // });
            }
            return newsfeeds;
        }
    }
}
