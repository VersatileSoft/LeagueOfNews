using MvvmCross.Navigation;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;
using System.Threading.Tasks;

namespace Surrender_20.UWP.ViewModels
{
    public class NewsfeedListViewModel : NewsfeedListCoreViewModel
    {
        public NewsfeedListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService, IMvxNavigationService navigationService)
           : base(newsfeedService, settingsService, navigationService)
        {
        }

        public override async Task Initialize()
        {
            await InitTabs();
        }

        protected override async Task NavigateToAsync(Newsfeed newsfeed)
        {
            await _navigationService.Navigate<NewsfeedItemViewModel, Newsfeed>(newsfeed);
        }
    }
}
