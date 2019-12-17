using LeagueOfNews.Core.Interface;
using LeagueOfNews.Model;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LeagueOfNews.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class NewsfeedListCoreViewModel : MvxViewModel
    {
        protected readonly INewsfeedService _newsfeedService;
        protected readonly ISettingsService _settingsService;
        protected readonly IMvxNavigationService _navigationService;

        protected NewsCategory SelectedCategory;

        public ObservableCollection<Newsfeed> Newsfeeds { get; set; }
        public string Title { get; set; }
        public bool IsLoading { get; set; } = false;
        public bool IsRefreshing { get; set; } = false;
        public bool IsLoadingMore { get; set; } = false;
        public ICommand ItemSelectedCommand { get; set; }
        public ICommand LoadMoreCommand { get; set; }
        public ICommand RefreshItemsCommand { get; set; }

        public NewsfeedListCoreViewModel(
            INewsfeedService newsfeedService,
            ISettingsService settingsService,
            IMvxNavigationService navigationService)
        {
            _newsfeedService = newsfeedService;
            _settingsService = settingsService;
            _navigationService = navigationService;

            ItemSelectedCommand = new MvxAsyncCommand<Newsfeed>(async (newsfeed) =>
            {
                await NavigateToAsync(newsfeed);
            });

            RefreshItemsCommand = new MvxCommand(RefreshNewsfeeds);
            LoadMoreCommand = new MvxCommand(LoadMoreNewsfeeds);
        }

        protected abstract Task NavigateToAsync(Newsfeed newsfeed);

        //TODO make 1 f instead of 3, maybe enum with sth like LoadingAction (load?, loadMore, refresh)
        protected void LoadNewsfeeds()
        {
            new Thread(async () =>
            {
                IsLoading = true;
                Newsfeeds = new ObservableCollection<Newsfeed>(await _newsfeedService.LoadNewsfeedsAsync(SelectedCategory)); //Sometimes prints error :/
                IsLoading = false;
            }).Start();
        }

        protected void LoadMoreNewsfeeds()
        {
            if (IsLoading || IsLoadingMore || IsRefreshing || SelectedCategory == NewsCategory.DevCorner)
            {
                return;
            }

            InvokeOnMainThread(async () =>
            {
                IsLoadingMore = true;
                foreach (Newsfeed item in await _newsfeedService.LoadMoreNewsfeeds(SelectedCategory))
                {
                    Newsfeeds.Add(item);
                }
                IsLoadingMore = false;
            });
        }

        protected void RefreshNewsfeeds()
        {
            InvokeOnMainThread(async () =>
            {
                IsRefreshing = true;
                Newsfeeds = new ObservableCollection<Newsfeed>(await _newsfeedService.LoadNewsfeedsAsync(SelectedCategory));
                IsRefreshing = false;
            });
        }
    }
}