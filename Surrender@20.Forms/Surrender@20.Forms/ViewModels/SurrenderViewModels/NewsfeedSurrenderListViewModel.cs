using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using Surrender_20.Core.Interface;

namespace Surrender_20.Forms.ViewModels.SurrenderViewModels
{
    public class NewsfeedSurrenderListViewModel : NewsfeedListViewModel
    {
        public NewsfeedSurrenderListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService, IMvxNavigationService navigationService, ITabsInitService tabsInitService) 
            : base(newsfeedService, settingsService, navigationService, tabsInitService)
        {
        }
    }
}
