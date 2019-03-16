using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.ViewModels;
using LeagueOfNews.Forms.Interfaces;
using LeagueOfNews.Model;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LeagueOfNews.Forms.ViewModels
{
    public class NewsfeedCategoryListViewModel : NewsfeedListCoreViewModel, IMvxViewModel<NewsCategory>
    {
        public NewsfeedCategoryListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService,
            IMvxNavigationService navigationService, ITabsInitService tabsInitService)
            : base(newsfeedService, settingsService, navigationService)
        {
            tabsInitService.TabsLoaded += (s, e) => InitTabs();
        }

        private void InitTabs()
        {
            LoadNewsfeeds();
        }

        protected override async Task NavigateToAsync(Newsfeed newsfeed)
        {
            await Browser.OpenAsync(newsfeed.UrlToNewsfeed, BrowserLaunchMode.SystemPreferred);
            //await _navigationService.Navigate<NewsfeedItemViewModel, Newsfeed>(newsfeed);
        }

        public void Prepare(NewsCategory parameter)
        {
            Title = _settingsService[parameter].Title;
            SelectedCategory = parameter;
        }
    }
}