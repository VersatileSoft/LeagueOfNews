using System;
using System.Threading.Tasks;
using LeagueOfNewsNew.XF.Services.Interfaces;
using PropertyChanged;

namespace LeagueOfNewsNew.XF.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class SplashPageModel : PageModelBase
    {
        private readonly IAppConfigService _remoteDataService;

        public event EventHandler NavigateToShell;

        public SplashPageModel(IAppConfigService remoteDataService) => _remoteDataService = remoteDataService;

        public override async Task OnLoad()
        {
            await _remoteDataService.LoadConfig();
            NavigateToShell?.Invoke(this, null);
        }
    }
}
