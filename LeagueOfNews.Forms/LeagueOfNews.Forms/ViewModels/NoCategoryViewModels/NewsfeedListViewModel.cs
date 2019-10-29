using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.ViewModels;
using LeagueOfNews.Forms.Interfaces;
using LeagueOfNews.Model;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

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
            IsOfficial = parameter == NewsCategory.Official ? true : false;

            LoadNewsfeeds();
        }

        protected override async Task NavigateToAsync(Newsfeed newsfeed)
        {
            await _chromeCustomTabService.StartChromCustomTab(newsfeed.UrlToNewsfeed);
        }
    }
}