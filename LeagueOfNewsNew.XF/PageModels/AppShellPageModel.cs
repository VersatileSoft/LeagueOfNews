using System.Collections.Generic;
using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.Services.Interfaces;

namespace LeagueOfNewsNew.XF.PageModels
{
    public class AppShellPageModel : PageModelBase
    {
        public IEnumerable<Website> Websites { get; set; }

        public AppShellPageModel(IAppConfigService appConfigService) => Websites = appConfigService.AppConfig.Websites;
    }
}
