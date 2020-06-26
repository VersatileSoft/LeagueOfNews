using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Firebase;
using Firebase.Messaging;
using Xamarin.Forms.Platform.Android;

namespace LeagueOfNewsNew.XF.Droid
{
    [Activity(MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        public const string CHANNEL_ID = "news_notification";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.SetTheme(Resource.Style.MainTheme);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Forms.FormsMaterial.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);

            FirebaseApp.InitializeApp(ApplicationContext);
            FirebaseMessaging.Instance.SubscribeToTopic("News");
            CreateNotificationChannel();

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }

            string name = Application.Context.Resources.GetString(Resource.String.channel_name);
            string description = Application.Context.Resources.GetString(Resource.String.channel_description);
            NotificationChannel channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            NotificationManager notificationManager = (NotificationManager)Application.Context.GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}