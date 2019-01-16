using Android.App;
using Android.App.Job;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Java.Lang;
using LabelHtml.Forms.Plugin.Droid;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using Surrender_20.Core;
using Surrender_20.Forms.Services;

namespace Surrender_20.Forms.Droid
{
    [Activity(Label = "SurrenderAt20", MainLauncher = true, Theme = "@style/MainTheme", NoHistory = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : MvxFormsAppCompatActivity<Setup, CoreApp, App>
    {

        public static readonly string CHANNEL_ID = "news_notification";

        protected override void OnCreate(Bundle bundle)
        {
            HtmlLabelRenderer.Initialize();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            CreateNotificationChannel();
            base.OnCreate(bundle);

            NavigateToRequestIfPresent(Intent);
            StartJob();
        }

        void StartJob()
        {
            Class javaClass = Class.FromType(typeof(NotificationJobService));
            ComponentName componentName = new ComponentName(this, javaClass);

            JobInfo info = new JobInfo.Builder(123, componentName)
               .SetMinimumLatency(20000)
               .SetOverrideDeadline(25000)
               .SetPersisted(true)
               .Build();


            JobScheduler jobScheduler = (JobScheduler)GetSystemService(JobSchedulerService);
            int result = jobScheduler.Schedule(info);
            if (result == JobScheduler.ResultSuccess)
            {
                Log.Info("MainActivity", "Job Sheduled");
            }
            else
            {
                Log.Info("MainActivity", "Job Filed Sheduled");
            }
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            NavigateToRequestIfPresent(intent);
        }

        protected void NavigateToRequestIfPresent(Intent intent)
        {
            // If MvxLaunchData is present, we then know we should navigate to that intent
            var requestText = intent.GetStringExtra("MvxLaunchData");

            if (requestText == null)
                return;

            var viewDispatcher = Mvx.IoCProvider.Resolve<IMvxViewDispatcher>();

            var converter = Mvx.IoCProvider.Resolve<IMvxNavigationSerializer>();
            var request = converter.Serializer.DeserializeObject<MvxViewModelRequest>(requestText);

            viewDispatcher.ShowViewModel(request);
        }

        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var name = Resources.GetString(Resource.String.channel_name);
            var description = GetString(Resource.String.channel_description);
            var channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}

