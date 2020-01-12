using LeagueOfNewsNew.XF.PageModels;
using Xamarin.Forms;

namespace LeagueOfNewsNew.XF.Pages
{
    public partial class AppShellPage : Shell
    {
        public AppShellPage()
        {
            InitializeComponent();
            this.SetPageModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((AppShellPageModel)BindingContext).LoadApp.Execute(null);
        }
    }
}