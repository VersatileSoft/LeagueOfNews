using MvvmCross.ViewModels;
using PropertyChanged;
using MvvmCross.Commands;
using System.Windows.Input;
using MvvmCross.Navigation;


namespace Surrender_20.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel<string>
    {
        public string Title { get; set; } = "Home";

        public ICommand HomeCommand { get; private set; }
        public ICommand PBECommand { get; private set; }
        public ICommand ReleasesCommand { get; private set; }
        public ICommand RedPostsCommand { get; private set; }
        public ICommand RotationsCommand { get; private set; }
        public ICommand EsportsCommand { get; private set; }

        public MenuViewModel(IMvxNavigationService navigationService) :
            base(navigationService)
        {

            //TODO change value IsPresented in MainPageViewModel when command execute 
            HomeCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>(""));
            PBECommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>(""));
            ReleasesCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>(""));
            RedPostsCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>(""));
            RotationsCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>(""));
            EsportsCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>(""));
        }
    }
}
