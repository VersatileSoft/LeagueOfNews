using MvvmCross.Platforms.Uap.Views;
using Surrender_20.UWP.ViewModels;
using Surrender_20.UWP.Views.Custom;

namespace Surrender_20.UWP.View
{
    public sealed partial class NewsfeedItemView : MvxUserControl<NewsfeedItemViewModel>
    {
        public NewsfeedItemView()
        {
            InitializeComponent();
        }
    }
}