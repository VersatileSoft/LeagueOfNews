using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.Services.Interfaces;
using RestSharp;

namespace LeagueOfNewsNew.XF.Services
{
    public class NewsfeedService : INewsfeedService
    {
        public async Task<IEnumerable<Newsfeed>> GetNewsfeeds(int websiteId, int page = 1)
        {
            RestClient client = new RestClient($"{App.API_URL}/newsfeed/{websiteId}?page={page}");

            IRestResponse<IEnumerable<Newsfeed>> response = await client.ExecuteAsync<IEnumerable<Newsfeed>>(new RestRequest());

            return response.Data;
        }
    }
}
