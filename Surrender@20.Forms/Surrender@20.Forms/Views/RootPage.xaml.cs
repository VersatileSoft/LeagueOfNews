using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Forms.ViewModels;

namespace Surrender_20.Forms.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false)]
    public partial class RootPage : MvxMasterDetailPage<RootViewModel>
    {
        public RootPage()
        {
            InitializeComponent();
        }
    }
}