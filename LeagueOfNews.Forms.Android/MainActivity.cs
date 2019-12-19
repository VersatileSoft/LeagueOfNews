using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading.Forms.Platform;
using LeagueOfNews.Core;
using LeagueOfNews.Core.Interface;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Views;
using Xamarin.Forms;

namespace LeagueOfNews.Forms.Droid
{
    [Activity(MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : MvxFormsAppCompatActivity<Setup, CoreApp, App>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.SetTheme(Resource.Style.MainTheme);

            CachedImageRenderer.Init(enableFastRenderer: true);
            FormsMaterial.Init(this, bundle);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
        }

        public override void InitializeApplication()
        {
            base.InitializeApplication();
            Mvx.IoCProvider.Resolve<INotificationService>().CreateNotificationChannel();
            Mvx.IoCProvider.Resolve<INotificationService>().RefreshNotificationJobService();
        }
    }
}