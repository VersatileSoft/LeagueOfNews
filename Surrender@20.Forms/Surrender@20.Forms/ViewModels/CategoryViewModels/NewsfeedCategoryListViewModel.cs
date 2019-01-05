using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;
using System.Threading.Tasks;

namespace Surrender_20.Forms.ViewModels
{
    public class NewsfeedCategoryListViewModel : NewsfeedListCoreViewModel, IMvxViewModel<Pages>
    {
        private string _url;

        public NewsfeedCategoryListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService,
            IMvxNavigationService navigationService, ITabsInitService tabsInitService)
            : base(newsfeedService, settingsService, navigationService)
        {
            tabsInitService.TabsLoaded += async (s, e) => await InitTabs();
        }

        private async Task InitTabs()
        {
            await LoadNewsfeeds(Pages.SurrenderHome, _url);
        }

        protected override async Task NavigateToAsync(Newsfeed newsfeed)
        {
            await _navigationService.Navigate<NewsfeedItemViewModel, Newsfeed>(newsfeed);
        }

        public void Prepare(Pages parameter)
        {
            Title = _settingsService[parameter].Title;
            _url = _settingsService[parameter].URL;
        }
    }
}
