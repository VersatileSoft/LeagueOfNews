using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;

namespace Surrender_20.Core.ViewModels
{
    public class NewsfeedItemViewModel : BaseViewModel
    {
        public string Title { get; set; } = "Weź coś zrób";

        public NewsfeedItemViewModel(IMvxNavigationService navigationService) :
            base(navigationService)
        {

        }
    }
}
