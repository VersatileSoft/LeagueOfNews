using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : MvxViewModel
    {     
        public string Title { get; set; } = "Home";
        private readonly IMvxNavigationService _navigationService;


        public ICommand HomeCommand { get; private set; }
        public ICommand PBECommand { get; private set; }
        public ICommand ReleasesCommand { get; private set; }
        public ICommand RedPostsCommand { get; private set; }
        public ICommand RotationsCommand { get; private set; }
        public ICommand EsportsCommand { get; private set; }

        public MainPageViewModel(IMvxNavigationService navigationService)
        {
            PBECommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>("Wklep url, lol"));

            HomeCommand = new MvxCommand(Placeholder);
            PBECommand = new MvxCommand(Placeholder);
            ReleasesCommand = new MvxCommand(Placeholder);
            RedPostsCommand = new MvxCommand(Placeholder);
            RotationsCommand = new MvxCommand(Placeholder);
            EsportsCommand = new MvxCommand(Placeholder);

            _navigationService = navigationService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

          //  MvxNotifyTask.Create(async () => await this.InitializeViewModels());
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<MenuViewModel>();
            await _navigationService.Navigate<NewsfeedListViewModel>();
        }

        void Placeholder()
        {

        }
    }  
}
