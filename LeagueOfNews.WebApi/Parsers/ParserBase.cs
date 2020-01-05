using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;
using LeagueOfNews.Model;

namespace LeagueOfNews.WebApi.Parsers
{
    public abstract class ParserBase
    {
        private readonly string _baseUrl;
        protected Website _website;
        protected ParserBase(Website website)
        {
            _baseUrl = "https://" + new Uri(website.Url).Host;
            _website = website;
        }

        protected abstract string _listNode { get; }
        protected abstract string GetUrl(int page);

        protected virtual Task<HtmlDocument> LoadDocument(int page) => new HtmlWeb().LoadFromWebAsync(GetUrl(page));

        public async Task<IEnumerable<Newsfeed>> Parse(int page)
        {
            List<Newsfeed> newsfeeds = new List<Newsfeed>();
            HtmlNodeCollection nodes = (await LoadDocument(page)).DocumentNode.SelectNodes(_listNode);

            foreach (HtmlNode node in nodes)
            {
                try
                {
                    newsfeeds.Add(ParseNewsfeed(node, _baseUrl));
                }
                catch
                {
                    continue;
                }
            }
            return newsfeeds;
        }

        protected abstract Newsfeed ParseNewsfeed(HtmlNode node, string baseUrl);
    }
}
