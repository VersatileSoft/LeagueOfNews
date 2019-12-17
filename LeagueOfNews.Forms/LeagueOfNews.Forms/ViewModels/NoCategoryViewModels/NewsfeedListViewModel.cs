using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.ViewModels;
using LeagueOfNews.Forms.Interfaces;
using LeagueOfNews.Model;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace LeagueOfNews.Forms.ViewModels
{
    public class NewsfeedListViewModel : NewsfeedListCoreViewModel, IMvxViewModel<NewsCategory>
    {
        private readonly IChromeCustomTabService _chromeCustomTabService;

        public bool IsOfficial { get; set; }

        public NewsfeedListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService, IMvxNavigationService navigationService, IChromeCustomTabService chromeCustomTabService)
            : base(newsfeedService, settingsService, navigationService)
        {
            _chromeCustomTabService = chromeCustomTabService;
        }

        public void Prepare(NewsCategory parameter)
        {
            Title = _settingsService[parameter].Title;
            SelectedCategory = parameter;
            IsOfficial = parameter == NewsCategory.Official;

            LoadNewsfeeds();
        }

        protected override async Task NavigateToAsync(Newsfeed newsfeed)
        {
            if (newsfeed.UrlToNewsfeed?.Contains("youtube") ?? false)
            {
                Uri urlFromRedirect = new Uri(newsfeed.UrlToNewsfeed);
                await Launcher.OpenAsync($"vnd.youtube:/{urlFromRedirect.PathAndQuery}");
            }
            else
            {
                await _chromeCustomTabService.StartChromeCustomTab(newsfeed.UrlToNewsfeed);
            }
        }
    }
}