using LeagueOfNews.Forms.ViewModels;
using LeagueOfNews.Model;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LeagueOfNews.Forms.Views.CategoryViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab, NoHistory = true)]
    public partial class NewsfeedCategoryListView : MvxContentPage<NewsfeedCategoryListViewModel>
    {
        public NewsfeedCategoryListView()
        {
            InitializeComponent();
        }

        private void MvxListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.ItemSelectedCommand.Execute((Newsfeed)e.Item);
        }
    }
}