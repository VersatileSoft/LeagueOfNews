using LeagueOfNews.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeagueOfNews.Core.Interface
{
    public interface INewsfeedService
    {
        Task<IList<Newsfeed>> LoadNewsfeedsAsync(NewsCategory page);
        Task<IList<Newsfeed>> LoadMoreNewsfeeds(NewsCategory page);
    }
}