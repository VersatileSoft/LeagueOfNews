using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Forms.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20.Forms.ViewModels
{
    public class RootViewModel : MainPageViewModel
    {
        public RootViewModel(IMvxNavigationService navigationService, IOperatingSystemService operatingSystemService, IMvxLog log, IMasterDetailService masterDetailServce) 
            : base(navigationService, operatingSystemService, log)
        {
            masterDetailServce.OnMasterPageSelect += OnMasterPageSelect;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            MvxNotifyTask.Create(async () => await this.InitializeViewModels());
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<MasterViewModel>();
            await NavigateTo(Setting.Home);
        }

        private void OnMasterPageSelect(object sender, MasterPageSelectArgs e)
        {
            NavCommand.Execute(e.Page);
        }
    }
}
