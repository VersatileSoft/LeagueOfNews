using MvvmCross.ViewModels;
using PropertyChanged;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedListViewModel : MvxViewModel<string>
    {
        [DoNotSetChanged] //TODO check if private access does not do that by default
        private string _url { get; set; }

        public override void Prepare(string parameter)
        {
            _url = parameter;
        }
    }
}
