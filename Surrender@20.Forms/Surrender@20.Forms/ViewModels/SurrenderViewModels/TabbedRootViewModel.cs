using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Forms.ViewModels.SurrenderViewModels;
using System;
using System.Threading.Tasks;

namespace Surrender_20.Forms.ViewModels
{
    public class TabbedRootViewModel : MainPageCoreViewModel
    {

        private bool _tabsLoaded = false;
        public ITabsInitService _tabsInitService;

        public TabbedRootViewModel(IMvxNavigationService navigationService, ITabsInitService tabsInitService, IOperatingSystemService operatingSystemService)
            : base(navigationService, operatingSystemService)
        {
            _tabsInitService = tabsInitService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            if (!_tabsLoaded)
                MvxNotifyTask.Create(async () => await InitializeViewModels());
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<NewsfeedSurrenderListViewModel, Setting>(Setting.Home);
            await _navigationService.Navigate<NewsfeedSurrenderListViewModel, Setting>(Setting.PBE);
            await _navigationService.Navigate<NewsfeedSurrenderListViewModel, Setting>(Setting.Releases);
            await _navigationService.Navigate<NewsfeedSurrenderListViewModel, Setting>(Setting.RedPosts);
            await _navigationService.Navigate<NewsfeedSurrenderListViewModel, Setting>(Setting.Rotations);
            await _navigationService.Navigate<NewsfeedSurrenderListViewModel, Setting>(Setting.ESports);

            _tabsLoaded = true;
            _tabsInitService.TabsLoaded.Invoke(this, EventArgs.Empty);
        }
    }
}
