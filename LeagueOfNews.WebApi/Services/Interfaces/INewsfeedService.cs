using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueOfNews.Model;

namespace LeagueOfNews.WebApi.Services.Interfaces
{
    public interface INewsfeedService
    {
        public Task<IEnumerable<Newsfeed>> GetNewsfeeds(int websiteId, int page);
    }
}