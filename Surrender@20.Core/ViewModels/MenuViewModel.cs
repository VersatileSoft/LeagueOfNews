using MvvmCross.ViewModels;
using PropertyChanged;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MenuViewModel : MvxViewModel
    {
        public string Title { get; set; } = "Home";
    }
}
