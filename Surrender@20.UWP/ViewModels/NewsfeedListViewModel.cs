using MvvmCross.Navigation;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20.UWP.ViewModels
{
    public class NewsfeedListViewModel : NewsfeedListCoreViewModel
    {
        public NewsfeedListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService, IMvxNavigationService navigationService, ITabsInitService tabsInitService) : base(newsfeedService, settingsService, navigationService, tabsInitService)
        {
        }

        protected override async Task NavigateToAsync(Newsfeed newsfeed)
        {
            await _navigationService.Navigate<NewsfeedItemViewModel, Newsfeed>(newsfeed);
        }
    }
}
