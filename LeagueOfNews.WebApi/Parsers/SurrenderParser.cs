using System.Web;
using HtmlAgilityPack;
using LeagueOfNews.Model;

namespace LeagueOfNews.WebApi.Parsers
{
    public class SurrenderParser : ParserBase
    {
        public SurrenderParser(Website website) : base(website) { }

        //TODO Somehow get right pages links from this stupid website :c
        protected override string GetUrl(int page) => _website.Url;
        protected override string _listNode => "//div[@class='post-outer']";

        protected override Newsfeed ParseNewsfeed(HtmlNode node, string baseUrl)
        {
            return new Newsfeed
            {
                Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//h1[@class='news-title']").InnerText).Replace("\n", ""),
                Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//span[@class='news-date']").InnerText).Replace("\n", ""),
                UrlToNewsfeed = node.SelectSingleNode(".//h1[@class='news-title']").SelectSingleNode(".//a").Attributes["href"].Value + "?m=1",
                ImageUrl = node.SelectSingleNode(".//img").Attributes["src"].Value,
                ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='news-content']").InnerText).Replace("\n", ""),
            };
        }
    }
}
