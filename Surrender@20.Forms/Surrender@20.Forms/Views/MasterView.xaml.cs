using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using LeagueOfNews.Forms.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LeagueOfNews.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master, NoHistory = true, WrapInNavigationPage = false)]
    public partial class MasterView : MvxContentPage<MasterViewModel>
    {
        public MasterView()
        {
            InitializeComponent();
        }

        private void MvxListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.NavigateCommand.Execute((MenuListElement)e.Item);
        }
    }
}