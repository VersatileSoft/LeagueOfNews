using System.Net;
using LeagueOfNewsNew.XF.Pages;
using Xamarin.Forms;

namespace LeagueOfNewsNew.XF
{
    public partial class App : Application
    {

        public const string API_URL = "https://myseriallist.ml/api";
        public App()
        {
            ServicePointManager.ServerCertificateValidationCallback +=
        (sender, certificate, chain, sslPolicyErrors) => true;
            IoC.Initialize();
            InitializeComponent();
            MainPage = new SplashPage();
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