using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    public class MainPageViewModel : BaseViewModel<string>
    {
        public string Title { get; set; } = "Home";
        private string _os;
        public string Os
        {
            get { return _os; }
            set { SetProperty(ref _os, value); }
        }

        public ICommand HomeCommand { get; private set; }
        public ICommand PBECommand { get; private set; }
        public ICommand ReleasesCommand { get; private set; }
        public ICommand RedPostsCommand { get; private set; }
        public ICommand RotationsCommand { get; private set; }
        public ICommand EsportsCommand { get; private set; }

        public ICommand NavViewCommand { get; private set; }

        public MainPageViewModel(IMvxNavigationService navigationService) :
            base(navigationService)
        {
            if (Os != "Android")
            {
                HomeCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>("Home"));
                PBECommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>("PBE"));
                ReleasesCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>("Red Posts"));
                RedPostsCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>(""));
                RotationsCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>(""));
                EsportsCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>(""));
            }   
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            if (Os == "Android")
                MvxNotifyTask.Create(async () => await this.InitializeViewModels());
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<NewsfeedListViewModel, string>("Home");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("PBE");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("Red Posts");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("People");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("E-Sports");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("Settings");
        }
    }
}
