using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Model;

namespace Surrender_20.Core.ViewModels
{
    public class NewsfeedItemViewModel : BaseViewModel
    {
        public Newsfeed Content { get; set; }
        public string Title => Content.Title;

        public NewsfeedItemViewModel(IMvxNavigationService navigationService) :
            base(navigationService)
        {

        }
    }
}
