using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using LeagueOfNews.Core;
using LeagueOfNews.Core.Interface;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Views;

namespace LeagueOfNews.Forms.Droid
{
    [Activity(Label = "League of News", MainLauncher = true, Theme = "@style/MyTheme.Splash", NoHistory = false, LaunchMode = LaunchMode.SingleTask, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MvxFormsAppCompatActivity<Setup, CoreApp, App>
    {
        protected override void OnCreate(Bundle bundle)
        {
            Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.SetTheme(Resource.Style.MainTheme);
            base.OnCreate(bundle);
        }

        public override void InitializeApplication()
        {
            base.InitializeApplication();
            MobileAds.Initialize(ApplicationContext, Resources.GetString(Resource.String.app_unit_id));
            INotificationService notificationService = Mvx.IoCProvider.Resolve<INotificationService>();
            notificationService.CreateNotificationChannel();
            notificationService.RefreshNotificationJobService();
        }
    }
}