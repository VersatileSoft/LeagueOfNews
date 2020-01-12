using System.Collections.ObjectModel;
using System.Windows.Input;
using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.Services.Interfaces;
using MvvmHelpers;
using PropertyChanged;

namespace LeagueOfNewsNew.XF.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class AppShellPageModel : ObservableObject
    {
        private readonly IRemoteDataService _remoteDataService;
        public string Title { get; set; } = "Siema";

        public ICommand LoadApp { get; set; }

        public ObservableCollection<Website> Websites { get; set; }

        public AppShellPageModel(IRemoteDataService remoteDataService)
        {
            _remoteDataService = remoteDataService;


            //TODO Idk why it's not working
            LoadApp = new AsyncCommand();
        }
    }
}
