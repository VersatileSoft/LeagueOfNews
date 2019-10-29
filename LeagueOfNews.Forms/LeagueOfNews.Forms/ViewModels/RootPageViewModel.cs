using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.ViewModels;
using LeagueOfNews.Forms.Interfaces;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using System;
using System.Threading.Tasks;

namespace LeagueOfNews.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class RootPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public event EventHandler HideMaster;

        public RootPageViewModel(IMvxNavigationService navigationService, IMasterDetailService masterDetailService)
        {
            _navigationService = navigationService;
            masterDetailService.OnMasterPageSelect += OnMasterPageSelect;
        }


        public override void ViewAppearing()
        {
            base.ViewAppearing();

            Task.Run(async () =>
            {
                await _navigationService.Navigate<MasterViewModel>();
                await _navigationService.Navigate<NewsfeedListViewModel, NewsCategory>(NewsCategory.Official);
            });
        }

        private void OnMasterPageSelect(object sender, MasterPageSelectArgs e)
        {
            HideMaster.Invoke(this, EventArgs.Empty);
            Task.Run(async () =>
            {
                switch (e.Page)
                {
                    case NewsWebsite.LoL:
                        await _navigationService.Navigate<NewsfeedListViewModel, NewsCategory>(NewsCategory.Official);
                        break;
                    case NewsWebsite.DevCorner:
                        await _navigationService.Navigate<NewsfeedListViewModel, NewsCategory>(NewsCategory.DevCorner);
                        break;
                    case NewsWebsite.Surrender:
                        await _navigationService.Navigate<TabbedRootViewModel>();
                        break;
                    case NewsWebsite.None:
                        await _navigationService.Navigate<SettingsViewModel>();
                        break;
                }
            });
        }
    }
}