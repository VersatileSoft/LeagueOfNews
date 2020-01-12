using System.Collections.Generic;
using LeagueOfNews.Model;

namespace LeagueOfNewsNew.XF.Services.Interfaces
{
    public interface IRemoteDataService
    {
        AppConfig GetAppConfig();
        IEnumerable<Newsfeed> GetNewsfeeds(int pageId, int page = 1);
    }
}
