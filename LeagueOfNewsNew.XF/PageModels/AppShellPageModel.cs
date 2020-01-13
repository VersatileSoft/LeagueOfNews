using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.Services.Interfaces;
using PropertyChanged;

namespace LeagueOfNewsNew.XF.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class AppShellPageModel : PageModelBase
    {
        private readonly IRemoteDataService _remoteDataService;
        public string Title { get; set; } = "Siema";
        public bool IsLoading { get; set; } = false;
        public ObservableCollection<Website> Websites { get; set; }

        public AppShellPageModel(IRemoteDataService remoteDataService) => _remoteDataService = remoteDataService;

        public override async Task OnLoad()
        {
            IsLoading = true;
            AppConfig appConfig = await _remoteDataService.GetAppConfig();
            Websites = new ObservableCollection<Website>(appConfig.Websites);
            IsLoading = false;
            Title = "Dupa";
        }
    }
}
