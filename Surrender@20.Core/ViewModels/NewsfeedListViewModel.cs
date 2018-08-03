using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedListViewModel : BaseViewModel<string>
    {
        [DoNotSetChanged] //TODO check if private access does not do that by default
        private string _url { get; set; }

        public string Title { get; set; }

        public NewsfeedListViewModel(IMvxNavigationService navigationService) :
           base(navigationService)
        {

        }

        public override void Prepare(string parameter)
        {
            Title = parameter;
        }
    }
}
