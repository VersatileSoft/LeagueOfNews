using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20.Forms.ViewModels
{
    public class RootPageViewModel : MainPageViewModel
    {

        private bool _tabsLoaded = false;
        
        public RootPageViewModel(IMvxNavigationService navigationService, ITabsInitService tabsInitService) 
            : base(navigationService, tabsInitService)
        {

        }


        public override void ViewAppearing()
        {
            base.ViewAppearing();

            if (!_tabsLoaded)
                MvxNotifyTask.Create(async () => await InitializeViewModels());
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.Home);
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.PBE);
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.Releases);
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.RedPosts);
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.Rotations);
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.ESports);

            _tabsLoaded = true;
            _tabsInitService.TabsLoaded.Invoke(this, EventArgs.Empty);
        }

    }
}
