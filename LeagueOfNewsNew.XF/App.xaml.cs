using LeagueOfNewsNew.XF.Pages;
using Xamarin.Forms;

namespace LeagueOfNewsNew.XF
{
    public partial class App : Application
    {
        public App()
        {
            IoC.Initialize();
            InitializeComponent();
            MainPage = new AppShellPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}