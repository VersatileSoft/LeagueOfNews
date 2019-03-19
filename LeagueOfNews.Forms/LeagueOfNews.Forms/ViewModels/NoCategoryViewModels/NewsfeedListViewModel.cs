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
    public class NewsfeedListViewModel : NewsfeedListCoreViewModel, IMvxViewModel<NewsCategory>
    {
        IChromeCustomTabService _chromeCustomTabService;

        public NewsfeedListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService, IMvxNavigationService navigationService, IChromeCustomTabService chromeCustomTabService)
            : base(newsfeedService, settingsService, navigationService)
        {
            _chromeCustomTabService = chromeCustomTabService;
        }

        public void Prepare(NewsCategory parameter)
        {
            Title = _settingsService[parameter].Title;
            SelectedCategory = parameter;
            LoadNewsfeeds();
        }

        protected override async Task NavigateToAsync(Newsfeed newsfeed)
        {
            //await Browser.OpenAsync(newsfeed.UrlToNewsfeed, BrowserLaunchMode.SystemPreferred);
            //await _navigationService.Navigate<NewsfeedItemViewModel, Newsfeed>(newsfeed);  
            await _chromeCustomTabService.StartChromCustomTab(newsfeed.UrlToNewsfeed);
        }
    }
}