using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Forms.ViewModels;
using Xamarin.Forms.Xaml;

namespace Surrender_20.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false, NoHistory = true)]
    public partial class RootPageView : MvxMasterDetailPage<RootPageViewModel>
    {
        public RootPageView()
        {
            InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            ViewModel.HideMaster += ViewModel_HideMaster;
        }

        private void ViewModel_HideMaster(object sender, System.EventArgs e)
        {
            IsPresented = false;          
        }
    }
}