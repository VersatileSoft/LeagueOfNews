using System.Collections.Generic;
using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.Services.Interfaces;
using RestSharp;

namespace LeagueOfNewsNew.XF.Services
{
    public class RemoteDataService : IRemoteDataService
    {

        private const string _apiUrl = "https://myseriallist.ml/api";

        public AppConfig GetAppConfig()
        {
            RestClient client = new RestClient($"{_apiUrl}/appconfig");

            IRestResponse<AppConfig> response = client.Execute<AppConfig>(new RestRequest());

            return response.Data;
        }

        public IEnumerable<Newsfeed> GetNewsfeeds(int websiteId, int page = 1)
        {
            RestClient client = new RestClient($"{_apiUrl}/newsfeed/{websiteId}?page={page}");

            IRestResponse<IEnumerable<Newsfeed>> response = client.Execute<IEnumerable<Newsfeed>>(new RestRequest());

            return response.Data;
        }
    }
}
