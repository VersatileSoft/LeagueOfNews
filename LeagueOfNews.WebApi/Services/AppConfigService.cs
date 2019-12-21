using LeagueOfNews.Model;
using LeagueOfNews.WebApi.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace LeagueOfNews.WebApi.Services
{
    public class AppConfigService : IAppConfigService
    {
        public AppConfig AppConfig { get; private set; }

        public AppConfigService(IOptions<AppConfig> appConfig) => AppConfig = appConfig.Value;
    }
}