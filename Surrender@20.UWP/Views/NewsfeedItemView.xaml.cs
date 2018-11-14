using MvvmCross.Platforms.Uap.Views;
using Surrender_20.UWP.ViewModels;

namespace Surrender_20.UWP.View
{
    public sealed partial class NewsfeedItemView : MvxWindowsPage
    {
        public NewsfeedItemViewModel VM => ViewModel as NewsfeedItemViewModel;

        public NewsfeedItemView()
        {
            this.InitializeComponent();
        }
    }
}
