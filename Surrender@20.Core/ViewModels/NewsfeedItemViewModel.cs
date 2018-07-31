using MvvmCross.ViewModels;
using PropertyChanged;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedItemViewModel : MvxViewModel
    {
        public string Title { get; set; } = "Weź coś zrób";
    }
}
