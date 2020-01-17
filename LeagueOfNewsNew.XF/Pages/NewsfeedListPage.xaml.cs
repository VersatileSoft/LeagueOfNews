using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.PageModels;
using Xamarin.Forms;

namespace LeagueOfNewsNew.XF.Pages
{
    public partial class NewsfeedListPage : ContentPage<NewsfeedListPageModel, int>
    {
        public NewsfeedListPage(int websiteId) : base(websiteId) => InitializeComponent();

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) => PageModel.ItemSelectedCommand.Execute((Newsfeed)e.SelectedItem);
    }
}