using System.Threading.Tasks;
using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.ViewModels;
using LeagueOfNews.Forms.Interfaces;
using LeagueOfNews.Model;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace LeagueOfNews.Forms.ViewModels
{
    public class NewsfeedCategoryListViewModel : NewsfeedListCoreViewModel, IMvxViewModel<NewsCategory>
    {
        private readonly IChromeCustomTabService _chromeCustomTabService;
        public NewsfeedCategoryListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService,
            IMvxNavigationService navigationService, ITabsInitService tabsInitService, IChromeCustomTabService chromeCustomTabService)
            : base(newsfeedService, settingsService, navigationService)
        {
            tabsInitService.TabsLoaded += (s, e) => InitTabs();
            _chromeCustomTabService = chromeCustomTabService;
        }

        private void InitTabs() => LoadNewsfeeds();

        protected override async Task NavigateToAsync(Newsfeed newsfeed) => await _chromeCustomTabService.StartChromeCustomTab(newsfeed.UrlToNewsfeed);

        public void Prepare(NewsCategory parameter)
        {
            Title = _settingsService[parameter].Title;
            SelectedCategory = parameter;
        }
    }
}