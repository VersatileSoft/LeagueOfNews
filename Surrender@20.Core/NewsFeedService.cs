using HtmlAgilityPack;
using Surrender_20.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Surrender_20
{
    public class NewsfeedService : INewsfeedService
    {

        public List<Newsfeed> LoadNewsfeeds()
        {
            List<Newsfeed> Newsfeeds = new List<Newsfeed>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("http://feeds.feedburner.com/surrenderat20/CqWw?format=html");
            List<HtmlNode> nodes = document.DocumentNode.SelectNodes("//li[@class='regularitem']").ToList();

            foreach (HtmlNode node in nodes)
            {
                Newsfeeds.Add(new Newsfeed()
                {
                    Title = HttpUtility.HtmlDecode(node.SelectNodes("./h4[@class='itemtitle']").First().InnerText),
                    Time = HttpUtility.HtmlDecode(node.SelectNodes("./h5[@class='itemposttime']").First().InnerText),
                    Content = node.SelectNodes("./div[@class='itemcontent']").First()
                });
            }

            return Newsfeeds;
        }
    }
}
