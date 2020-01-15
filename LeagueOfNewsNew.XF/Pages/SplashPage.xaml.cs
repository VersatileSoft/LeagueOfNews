using LeagueOfNewsNew.XF.PageModels;
using Xamarin.Forms.Xaml;

namespace LeagueOfNewsNew.XF.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashPage : ContentPage<SplashPageModel>
    {
        public SplashPage()
        {
            InitializeComponent();
            PageModel.NavigateToShell += PageModel_NavigateToShell;
        }

        private void PageModel_NavigateToShell(object sender, System.EventArgs e) => App.Current.MainPage = new AppShellPage();
    }
}