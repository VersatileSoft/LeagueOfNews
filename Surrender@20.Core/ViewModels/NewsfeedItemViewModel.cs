using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Model;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedItemViewModel : MvxViewModel
    {
        public Newsfeed Content { get; set; }
        public string Title => Content.Title;

        public NewsfeedItemViewModel()
        {

        }
    }
}
