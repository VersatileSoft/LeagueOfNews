using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Forms.Interfaces;

namespace Surrender_20.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class RootViewModel : MainPageViewModel
    {
        public RootViewModel(IMvxNavigationService navigationService, IOperatingSystemService operatingSystemService, IMasterDetailService masterDetailService) 
            : base(navigationService, operatingSystemService)
        {
            masterDetailService.OnMasterPageSelect += OnMasterPageSelect;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            MvxNotifyTask.Create(async ()  => {
                await _navigationService.Navigate<MasterViewModel>();
                await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.Home);
            });
        }

        private void OnMasterPageSelect(object sender, MasterPageSelectArgs e)
        {
            NavCommand.Execute(e.Page);
        }
    }
}
