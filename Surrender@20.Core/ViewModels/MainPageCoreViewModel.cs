using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageCoreViewModel : MvxViewModel
    {

        public IMvxNavigationService _navigationService;
        public IOperatingSystemService _operatingSystemService;

        public MainPageCoreViewModel(IMvxNavigationService navigationService, IOperatingSystemService operatingSystemService)
        {
            _navigationService = navigationService;
            _operatingSystemService = operatingSystemService;
        }
    }
}
