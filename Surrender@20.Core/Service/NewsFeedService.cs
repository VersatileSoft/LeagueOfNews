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
            List<Newsfeed> newsfeeds = new List<Newsfeed>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url);

            var nodes = document.DocumentNode.SelectNodes("//li[@class='regularitem']");
            foreach (HtmlNode node in nodes)
            {
                newsfeeds.Add(new Newsfeed()
                {
                    Title = HttpUtility.HtmlDecode(node.SelectSingleNode("./h4[@class='itemtitle']").InnerText),
                    Time = HttpUtility.HtmlDecode(node.SelectSingleNode("./h5[@class='itemposttime']").InnerText),
                    Content = node.SelectSingleNode("./div[@class='itemcontent']")
                });
            }

            return newsfeeds;
        }
    }
}
