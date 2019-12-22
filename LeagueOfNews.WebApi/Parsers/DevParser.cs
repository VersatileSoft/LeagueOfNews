using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;
using LeagueOfNews.Model;

namespace LeagueOfNews.WebApi.Parsers
{
    public class DevParser : ParserBase
    {
        public DevParser(Website website) : base(website) { }

        protected override string _listNode => "/tr[@class='discussion-list-item row no-voting has-rioter-comments']";

        protected override string GetUrl(int page) => _website.Url + $"&num_loaded={(page - 1) * 50}";

        protected override async Task<HtmlDocument> LoadDocument(int page)
        {
            HttpClient client = new HttpClient();
            string html = "";
            client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.88 Safari/537.36");
            HttpResponseMessage response = await client.GetAsync(GetUrl(page));
            if (response.IsSuccessStatusCode)
            {
                html = (await response.Content.ReadAsAsync<DevPage>()).Discussions;
            }
            html = HttpUtility.HtmlDecode(html);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }

        protected override Newsfeed ParseNewsfeed(HtmlNode node, string baseUrl)
        {
            return new Newsfeed
            {
                Title = HttpUtility.HtmlDecode(node.SelectSingleNode(".//td[@class='title']/div/a/span").InnerText.Replace("\n", "")),
                Date = "By " + HttpUtility.HtmlDecode(node.SelectSingleNode(".//span[@class='username']").InnerText) + " " +
                        DateTime.Parse(node.SelectSingleNode(".//span[@class='timeago']").Attributes["title"].Value).ToShortDateString(),
                UrlToNewsfeed = baseUrl + node.SelectSingleNode(".//td[@class='title']/div/a").Attributes["href"].Value,
                ShortDescription = HttpUtility.HtmlDecode(node.SelectSingleNode(".//td[@class='title']/div/a/span").Attributes["title"].Value).Replace("\n", "").Replace("-", "").Replace("\r", ""),
            };
        }
    }

    internal class DevPage
    {
        public string Discussions { get; set; }
        public int TotalDiscussionsSoft { get; set; }
        public int ResultsCount { get; set; }
    }
}
