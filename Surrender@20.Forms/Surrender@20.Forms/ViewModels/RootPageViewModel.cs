using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Forms.Interfaces;
using Surrender_20.Forms.ViewModels.OfficialViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class RootPageViewModel : MvxViewModel
    {

        private IMvxNavigationService _navigationService;

        public event EventHandler HideMaster;

        public RootPageViewModel(IMvxNavigationService navigationService, IOperatingSystemService operatingSystemService, IMasterDetailService masterDetailService)
        //: base(navigationService, operatingSystemService)
        {
            _navigationService = navigationService;
            masterDetailService.OnMasterPageSelect += OnMasterPageSelect;           
        }


        public override void ViewAppearing()
        {
            base.ViewAppearing();

            MvxNotifyTask.Create(async () =>
            {
                await _navigationService.Navigate<MasterViewModel>();
                await _navigationService.Navigate<TabbedRootViewModel>();
            });
        }

        private void OnMasterPageSelect(object sender, MasterPageSelectArgs e)
        {           
            HideMaster.Invoke(this, EventArgs.Empty);
            MvxNotifyTask.Create(async () =>
            {
                switch (e.Page)
                {
                    case Pages.SurrenderHome : await _navigationService.Navigate<TabbedRootViewModel>(); break;
                    case Pages.Official: await _navigationService.Navigate<NewsfeedListViewModel, Pages>(Pages.SurrenderHome); break;
                }
            });        
        }
    }
}
