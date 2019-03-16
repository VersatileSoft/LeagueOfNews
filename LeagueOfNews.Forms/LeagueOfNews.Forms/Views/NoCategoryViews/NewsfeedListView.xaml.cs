using LeagueOfNews.Forms.ViewModels;
using LeagueOfNews.Model;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LeagueOfNews.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true)]
    public partial class NewsfeedListView : MvxContentPage<NewsfeedListViewModel>
    {
        public NewsfeedListView()
        {
            InitializeComponent();
        }

        private void MvxListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.ItemSelectedCommand.Execute((Newsfeed)e.Item);
        }
    }
}