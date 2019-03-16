using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using LeagueOfNews.Forms.ViewModels;
using Xamarin.Forms.Xaml;

namespace LeagueOfNews.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class TabbedRootPage : MvxTabbedPage<TabbedRootViewModel>
    {
        public TabbedRootPage()
        {
            InitializeComponent();
        }
    }
}