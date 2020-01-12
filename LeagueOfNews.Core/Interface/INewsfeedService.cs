using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueOfNews.Model;

namespace LeagueOfNews.Core.Interface
{
    public interface INewsfeedService
    {
        Task<IList<Newsfeed>> LoadNewsfeedsAsync(NewsCategory page);
        Task<IList<Newsfeed>> LoadMoreNewsfeeds(NewsCategory page);
    }
}