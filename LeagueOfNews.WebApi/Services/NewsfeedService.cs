using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeagueOfNews.Model;
using LeagueOfNews.WebApi.Parsers;
using LeagueOfNews.WebApi.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace LeagueOfNews.WebApi.Services
{
    public class NewsfeedService : INewsfeedService
    {
        private readonly AppConfig _appConfig;
        public NewsfeedService(IOptions<AppConfig> appConfig) => _appConfig = appConfig.Value;

        public async Task<IEnumerable<Newsfeed>> GetNewsfeeds(int websiteId, int page)
        {
            Website website = _appConfig.Websites
                .Concat(_appConfig.Websites.Where(w => w.Subpages != null).SelectMany(w => w.Subpages).AsEnumerable())
                .Where(w => w.Id == websiteId)
                .FirstOrDefault();

            ParserBase parser = (ParserBase)Activator.CreateInstance(typeof(ParserBase).Assembly.GetTypes().Where(t => t.Name == website.ParserName + "Parser").FirstOrDefault(), website);

            return await parser.Parse(page);
        }
    }
}
