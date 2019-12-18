using LeagueOfNews.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueOfNews.WebApi.Services.Interfaces
{
    public interface IAppConfigService
    {
        public AppConfig AppConfig { get; }
    }
}
