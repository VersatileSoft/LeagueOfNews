using System;
using System.Threading.Tasks;
using LeagueOfNews.Core.Interface;
using LeagueOfNews.Forms.Interfaces;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace LeagueOfNews.Forms.ViewModels
{
    public class TabbedRootViewModel : MvxViewModel
    {
        private bool _tabsLoaded = false;
        private readonly ITabsInitService _tabsInitService;
        private readonly IMvxNavigationService _navigationService;

        public TabbedRootViewModel(ITabsInitService tabsInitService, IMvxNavigationService navigationService)
        {
            _tabsInitService = tabsInitService;
            _navigationService = navigationService;
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