using Surrender_20.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surrender_20.Core.Interface
{
    public interface INewsfeedService
    {
        Task<IList<Newsfeed>> LoadNewsfeedsAsync(NewsCategory page);
        Task<IList<Newsfeed>> LoadMoreNewsfeeds(NewsCategory page);
    }
}