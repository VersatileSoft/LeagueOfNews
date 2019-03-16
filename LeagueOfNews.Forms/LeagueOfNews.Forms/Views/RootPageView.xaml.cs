using LeagueOfNews.Forms.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace LeagueOfNews.Forms.Views
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