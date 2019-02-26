using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class NewsfeedListCoreViewModel : MvxViewModel
    {
        protected INewsfeedService _newsfeedService;
        protected ISettingsService _settingsService;
        protected IMvxNavigationService _navigationService;
        protected NewsCategory _page;

        public ObservableCollection<Newsfeed> Newsfeeds { get; set; }
        public string Title { get; set; }
        public bool IsLoading { get; set; }
        public bool IsRefreshing { get; set; }
        public bool IsLoadingMore { get; set; }
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

            ItemSelectedCommand = new MvxAsyncCommand<Newsfeed>(async (Newsfeed) =>
            {
                await NavigateToAsync(Newsfeed);
            });

            RefreshItemsCommand = new MvxCommand(RefreshNewsfeeds);
            LoadMoreCommand = new MvxCommand(LoadMoreNewsfeeds);
        }

        protected abstract Task NavigateToAsync(Newsfeed newsfeed);

        protected void LoadNewsfeeds() //TODO Add Page parameter to make it more universal
        {
            new Thread(async () =>
            {
                IsLoading = true;
                Newsfeeds = new ObservableCollection<Newsfeed>(await _newsfeedService.LoadNewsfeedsAsync(_page));
                IsLoading = false;
            }).Start();
        }

        protected void LoadMoreNewsfeeds()
        {
            new Thread(async () =>
            {
                IsLoadingMore = true;
                foreach (Newsfeed item in await _newsfeedService.LoadMoreNewsfeeds(_page))
                {
                    Newsfeeds.Add(item);
                }
                IsLoadingMore = false;
            }).Start();
        }
        protected void RefreshNewsfeeds()
        {
            Newsfeeds.Clear();
            new Thread(async () =>
            {
                IsRefreshing = true;
                Newsfeeds = new ObservableCollection<Newsfeed>(await _newsfeedService.LoadNewsfeedsAsync(_page));
                IsRefreshing = false;
            }).Start();
        }
    }
}