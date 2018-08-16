using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Surrender_20.Core.Service
{
    public class NewsfeedService : INewsfeedService
    {

        public async Task<ObservableCollection<Newsfeed>> LoadNewsfeedsAsync(string url)
        {
            ObservableCollection<Newsfeed> newsfeeds = new ObservableCollection<Newsfeed>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = await web.LoadFromWebAsync(url);

            var nodes = document.DocumentNode.SelectNodes("//li[@class='regularitem']");
            foreach (HtmlNode node in nodes)
            {
                newsfeeds.Add(new Newsfeed()
                {
                    Title = HttpUtility.HtmlDecode(node.SelectSingleNode("./h4[@class='itemtitle']").InnerText),
                    Time = HttpUtility.HtmlDecode(node.SelectSingleNode("./h5[@class='itemposttime']").InnerText),
                    Content = node.SelectSingleNode("./div[@class='itemcontent']"),
                    Image = node.SelectSingleNode(".//img").Attributes["src"].Value.ToString(),
                    ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode("./div[@class='itemcontent']").SelectSingleNode("./div").InnerText)
                });
            }

            return newsfeeds;
        }
    }
}
