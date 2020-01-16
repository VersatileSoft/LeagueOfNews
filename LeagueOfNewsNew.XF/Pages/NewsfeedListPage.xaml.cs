using LeagueOfNewsNew.XF.PageModels;

namespace LeagueOfNewsNew.XF.Pages
{
    public partial class NewsfeedListPage : ContentPage<NewsfeedListPageModel, int>
    {
        public NewsfeedListPage(int websiteId) : base(websiteId) => InitializeComponent();
    }
}