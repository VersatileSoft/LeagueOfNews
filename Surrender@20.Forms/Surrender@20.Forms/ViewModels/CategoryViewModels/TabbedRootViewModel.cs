using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
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

        public override void ViewAppeared()
        {
            base.ViewAppeared();

            if (!_tabsLoaded)
            {
                MvxNotifyTask.Create(async () => await InitializeViewModels());
            }
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, Pages>(Pages.SurrenderHome);
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, Pages>(Pages.PBE);
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, Pages>(Pages.Releases);
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, Pages>(Pages.RedPosts);
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, Pages>(Pages.Rotations);
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, Pages>(Pages.ESports);

            _tabsLoaded = true;
            _tabsInitService.TabsLoaded.Invoke(this, EventArgs.Empty);
        }
    }
}
