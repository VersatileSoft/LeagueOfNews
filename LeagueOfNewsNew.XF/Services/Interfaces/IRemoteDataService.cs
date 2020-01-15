using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueOfNews.Model;

namespace LeagueOfNewsNew.XF.Services.Interfaces
{
    public interface IRemoteDataService
    {
        Task<IEnumerable<Newsfeed>> GetNewsfeeds(int pageId, int page = 1);
    }
}
