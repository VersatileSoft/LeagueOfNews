using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    public class MainPageViewModel : BaseViewModel
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
                HomeCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "Home", "url" } })));
                PBECommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "PBE", "url" } })));
                ReleasesCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "Red Posts", "url" } })));
                RedPostsCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "People", "url" } })));
                RotationsCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "E-Sports", "url" } })));
                EsportsCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "Settings", "url" } })));
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
            await _navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "Home", "http://feeds.feedburner.com/surrenderat20/CqWw?format=html" } }));
            await _navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "PBE", "url" } }));
            await _navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "Red Posts", "url" } }));
            await _navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "People", "url" } }));
            await _navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "E-Sports", "url" } }));
            await _navigationService.Navigate<NewsfeedListViewModel, MvxBundle>(new MvxBundle(new Dictionary<string, string> { { "Settings", "url" } }));
        }
    }
}
