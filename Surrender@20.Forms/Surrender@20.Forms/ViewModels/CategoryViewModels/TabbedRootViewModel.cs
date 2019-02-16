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
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, NewsCategory>(NewsCategory.SurrenderHome);
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, NewsCategory>(NewsCategory.PBE);
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, NewsCategory>(NewsCategory.Releases);
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, NewsCategory>(NewsCategory.RedPosts);
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, NewsCategory>(NewsCategory.Rotations);
            await _navigationService.Navigate<NewsfeedCategoryListViewModel, NewsCategory>(NewsCategory.ESports);

            _tabsLoaded = true;
            _tabsInitService.TabsLoaded.Invoke(this, EventArgs.Empty);
        }
    }
}