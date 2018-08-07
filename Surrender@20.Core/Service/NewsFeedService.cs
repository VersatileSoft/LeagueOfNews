using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Surrender_20.Core.Service
{
    public class NewsfeedService : INewsfeedService
    {

        public List<Newsfeed> LoadNewsfeeds(string url)
        {
            List<Newsfeed> Newsfeeds = new List<Newsfeed>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);
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
