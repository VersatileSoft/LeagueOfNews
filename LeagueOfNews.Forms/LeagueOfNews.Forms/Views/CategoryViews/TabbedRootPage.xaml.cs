using LeagueOfNews.Forms.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace LeagueOfNews.Forms.Views.CategoryViews
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = false)]
    public partial class TabbedRootPage : MvxTabbedPage<TabbedRootViewModel>
    {
        public TabbedRootPage()
        {
            InitializeComponent();
        }
    }
}