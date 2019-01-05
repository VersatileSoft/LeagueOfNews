using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Forms.ViewModels;
using Xamarin.Forms.Xaml;

namespace Surrender_20.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Root, WrapInNavigationPage = false, NoHistory = true)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail)]
    public partial class TabbedRootPage : MvxTabbedPage<TabbedRootViewModel>
    {
        public TabbedRootPage()
        {
            InitializeComponent();
        }
    }
}