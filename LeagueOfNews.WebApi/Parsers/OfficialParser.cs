using System.Web;
using HtmlAgilityPack;
using LeagueOfNews.Model;

namespace LeagueOfNews.WebApi.Parsers
{
    public class OfficialParser : ParserBase
    {
        public OfficialParser(Website website) : base(website) { }

        protected override string GetUrl(int page) => page == 1 ? _website.Url : _website.Url + $"?page={page - 1}";
        protected override string _listNode => "//div[@class='gs-container']";

        protected override Newsfeed ParseNewsfeed(HtmlNode node, string baseUrl)
        {
            return new Newsfeed
            {
                Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").InnerText).Replace("\n", ""),
                Date = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='horizontal-group']").InnerText).Replace("\n", ""),
                UrlToNewsfeed = baseUrl + node.SelectSingleNode(".//div[@class='default-2-3']").SelectSingleNode(".//a").Attributes["href"].Value,
                ImageUrl = baseUrl + node.SelectSingleNode(".//img").Attributes["src"].Value,
                ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//div[@class='teaser-content']").InnerText).Replace("\n", ""),
            };
        }
    }
}
