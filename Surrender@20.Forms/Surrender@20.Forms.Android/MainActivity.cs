using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using Surrender_20.Core;
using Surrender_20.Core.Interface;

namespace Surrender_20.Forms.Droid
{
    [Activity(Label = "League of News", MainLauncher = true, Theme = "@style/MainTheme", NoHistory = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MvxFormsAppCompatActivity<Setup, CoreApp, App>
    {
        public static readonly string CHANNEL_ID = "news_notification";

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            NavigateToRequestIfPresent(Intent);
        }

        public override void InitializeApplication()
        {
            base.InitializeApplication();
            MobileAds.Initialize(ApplicationContext, Resources.GetString(Resource.String.app_unit_id));
            INotificationService notificationService = Mvx.IoCProvider.Resolve<INotificationService>();
            notificationService.CreateNotificationChannel();
            notificationService.RefreshNotificationJobService();
            Mvx.IoCProvider.Resolve<IThemeService>().SetAppTheme(Mvx.IoCProvider.Resolve<ISaveDataService>().GetIsDarkTheme() ? AppTheme.Dark : AppTheme.Ligt);

        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            NavigateToRequestIfPresent(intent);
        }

        protected void NavigateToRequestIfPresent(Intent intent)
        {
            // If MvxLaunchData is present, we then know we should navigate to that intent
            string requestText = intent.GetStringExtra("Request");

            if (requestText == null)
            {
                return;
            }

            IMvxViewDispatcher viewDispatcher = Mvx.IoCProvider.Resolve<IMvxViewDispatcher>();

            IMvxNavigationSerializer converter = Mvx.IoCProvider.Resolve<IMvxNavigationSerializer>();
            MvxViewModelRequest request = converter.Serializer.DeserializeObject<MvxViewModelRequest>(requestText);

            viewDispatcher.ShowViewModel(request);
        }
    }
}