using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using System.Linq;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedListViewModel : BaseViewModel
    {
        [DoNotSetChanged] //TODO check if private access does not do that by default
        private string _url { get; set; }

        public string Title { get; set; }

        public NewsfeedListViewModel(IMvxNavigationService navigationService) :
           base(navigationService)
        {

        }

        public override void Prepare(MvxBundle parameter)
        {
            Title = parameter.Data.Keys.ToList()[0];
            _url = parameter.Data.Values.ToList()[0];
        }
    }
}
