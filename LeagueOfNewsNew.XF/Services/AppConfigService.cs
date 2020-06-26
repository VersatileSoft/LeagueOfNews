using System.Threading.Tasks;
using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.Services.Interfaces;
using RestSharp;

namespace LeagueOfNewsNew.XF.Services
{
    public class AppConfigService : IAppConfigService
    {
        public AppConfig AppConfig { get; set; }

        public async Task LoadConfig()
        {
            RestClient client = new RestClient($"{App.API_URL}/appconfig");
            IRestResponse<AppConfig> response = await client.ExecuteAsync<AppConfig>(new RestRequest());
            AppConfig = response.Data;
        }
    }
}
