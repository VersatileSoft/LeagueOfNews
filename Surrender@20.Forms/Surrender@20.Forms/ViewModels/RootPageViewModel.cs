using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Forms.Interfaces;
using System;
using System.Threading.Tasks;

namespace Surrender_20.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class RootPageViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public event EventHandler HideMaster;

        public RootPageViewModel(IMvxNavigationService navigationService, IMasterDetailService masterDetailService)
        //: base(navigationService, operatingSystemService)
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
                    case NewsCategory.SurrenderHome: await _navigationService.Navigate<TabbedRootViewModel>(); break;
                    // case Pages.Dev:
                    case NewsCategory.Official: await _navigationService.Navigate<NewsfeedListViewModel, NewsCategory>(e.Page); break;
                    case NewsCategory.None: await _navigationService.Navigate<SettingsViewModel>(); break;
                }
            });
        }
    }
}