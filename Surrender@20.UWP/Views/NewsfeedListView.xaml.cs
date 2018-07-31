using MvvmCross.Platforms.Uap.Views;
using Surrender_20.Core.ViewModels;

namespace Surrender_20.View
{
    public sealed partial class NewsfeedListView : MvxWindowsPage
    {
        public NewsfeedListViewModel VM => ViewModel as NewsfeedListViewModel;

        public NewsfeedListView()
        {
            this.InitializeComponent();
        }
    }
}
