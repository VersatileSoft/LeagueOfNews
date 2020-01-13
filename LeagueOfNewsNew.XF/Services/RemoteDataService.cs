using System.Collections.Generic;
using System.Threading.Tasks;
using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.Services.Interfaces;
using RestSharp;

namespace LeagueOfNewsNew.XF.Services
{
    public class RemoteDataService : IRemoteDataService
    {

        private const string _apiUrl = "https://myseriallist.ml/api";

        public async Task<AppConfig> GetAppConfig()
        {
            RestClient client = new RestClient($"{_apiUrl}/appconfig");

            IRestResponse<AppConfig> response = await client.ExecuteAsync<AppConfig>(new RestRequest());

            return response.Data;
        }

        public async Task<IEnumerable<Newsfeed>> GetNewsfeeds(int websiteId, int page = 1)
        {
            RestClient client = new RestClient($"{_apiUrl}/newsfeed/{websiteId}?page={page}");

            IRestResponse<IEnumerable<Newsfeed>> response = await client.ExecuteAsync<IEnumerable<Newsfeed>>(new RestRequest());

            return response.Data;
        }
    }
}
