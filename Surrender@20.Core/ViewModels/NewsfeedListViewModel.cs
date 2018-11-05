using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedListViewModel : MvxViewModel<Setting>
    {
        private string _url;
        private INewsfeedService _newsfeedService;
        private ISettingsService _settingsService;
        private IMvxNavigationService _navigationService;

        public ObservableCollection<Newsfeed> Newsfeeds { get; set; }
        public string Title { get; set; }
        public bool IsLoading { get; set; }
        public ICommand ItemSelected { get; set; }
        public ICommand LoadMore { get; set; }

        public NewsfeedListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService, 
            IMvxNavigationService navigationService, ITabsInitService tabsInitService)
        {
            _newsfeedService = newsfeedService;
            _settingsService = settingsService;
            _navigationService = navigationService;
            tabsInitService.TabsLoaded += async (s, e) => await InitTabs();

            ItemSelected = new MvxAsyncCommand<Newsfeed>((Newsfeed) =>
            {
                return NavigateTo(Newsfeed);
            });

            LoadMore = new MvxAsyncCommand(async() =>
            {
                foreach(var item in await _newsfeedService.LoadMoreNewsfeeds())
                {
                    IsLoading = true;
                    Newsfeeds.Add(item);
                    IsLoading = false;
                }
            });
        }

        protected async Task NavigateTo(Newsfeed newsfeed)
        {
            await _navigationService.Navigate<NewsfeedItemViewModel, Newsfeed>(newsfeed);
        }

        public override void Prepare(Setting parameter)
        {
            Title = _settingsService[parameter].Title;
            _url = _settingsService[parameter].URL;
        }

        private async Task InitTabs()
        {
            IsLoading = true;
            Newsfeeds = await _newsfeedService.LoadNewsfeedsAsync(_url);
            IsLoading = false;
        }
    }
}
