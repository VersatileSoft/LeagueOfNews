using MvvmCross.Platforms.Uap.Views;
using Surrender_20.Core.ViewModels;

namespace Surrender_20.UWP.View
{
    public sealed partial class NewsfeedItemView : MvxWindowsPage
    {
        public NewsfeedItemCoreViewModel VM => ViewModel as NewsfeedItemCoreViewModel;

        public NewsfeedItemView()
        {
            this.InitializeComponent();
        }
    }
}
