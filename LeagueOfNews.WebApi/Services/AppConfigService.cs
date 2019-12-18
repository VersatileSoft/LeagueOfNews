using LeagueOfNews.Model;
using LeagueOfNews.WebApi.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueOfNews.WebApi.Services
{
    public class AppConfigService : IAppConfigService
    {
        public AppConfig AppConfig{ get; private set; }

        public AppConfigService(IOptions<AppConfig> appConfig)
        {
            AppConfig = appConfig.Value;
        }
    }
}
