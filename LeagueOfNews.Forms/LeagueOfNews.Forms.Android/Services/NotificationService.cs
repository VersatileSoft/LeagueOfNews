using Android.App;
using Android.App.Job;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.CustomTabs;
using Android.Support.V4.App;
using Android.Util;
using Java.Lang;
using LeagueOfNews.Core.Interface;
using LeagueOfNews.Forms.Services;
using LeagueOfNews.Model;
using MvvmCross;
using System;
using Application = Android.App.Application;
using Uri = Android.Net.Uri;

namespace LeagueOfNews.Forms.Droid.Services
{
    public class NotificationService : INotificationService
    {
        private static readonly string CHANNEL_ID = "news_notification";

        public void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            string name = Application.Context.Resources.GetString(Resource.String.channel_name);
            string description = Application.Context.Resources.GetString(Resource.String.channel_description);
            NotificationChannel channel = new NotificationChannel(CHANNEL_ID, name, NotificationImportance.Default)
            {
                Description = description
            };

            NotificationManager notificationManager = (NotificationManager)Application.Context.GetSystemService(Context.NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        public void ShowNewPostNotification(Newsfeed newsfeed, NewsWebsite page)
        {
            Notification notification = new NotificationCompat.Builder(Application.Context, CHANNEL_ID)
                .SetContentTitle(newsfeed.Title)
                .SetContentText(newsfeed.ShortDescription)
                .SetSmallIcon(Resource.Drawable.NotificationIcon)
                .SetContentIntent(GetContentIntent(newsfeed))
                .SetShowWhen(true)
                .SetAutoCancel(true)
                .Build();

            NotificationManagerCompat notificationManager = NotificationManagerCompat.From(Application.Context);

            switch (page)
            {
                case NewsWebsite.LoL:
                    notificationManager.Notify(1000, notification);
                    break;

                case NewsWebsite.DevCorner:
                    notificationManager.Notify(1001, notification);
                    break;

                case NewsWebsite.Surrender:
                    notificationManager.Notify(1002, notification);
                    break;
            }
        }

        public void RefreshNotificationJobService()
        {
            ISettingsService _settings = Mvx.IoCProvider.Resolve<ISettingsService>();
            if (_settings.HasNotificationsEnabled)
            {
                StartNotificationJobService(_settings.NewPostCheckFrequency);
            }
            else
            {
                StopNotificationJobService();
            }
        }

        private void StartNotificationJobService(int HoursFrequency)
        {
            Class javaClass = Class.FromType(typeof(NotificationJobService));
            ComponentName componentName = new ComponentName(Application.Context, javaClass);

            JobInfo info = new JobInfo.Builder(123, componentName)
               .SetMinimumLatency(HoursFrequency * 60 * 60 * 1000)
               .SetOverrideDeadline(HoursFrequency * 60 * 60 * 1000)
               .SetPersisted(true)
               .Build();

            JobScheduler jobScheduler = (JobScheduler)Application.Context.GetSystemService(Context.JobSchedulerService);
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

        public void StopNotificationJobService()
        {
            JobScheduler jobScheduler = (JobScheduler)Application.Context.GetSystemService(Context.JobSchedulerService);
            jobScheduler.Cancel(123);
        }

        private PendingIntent GetContentIntent(Newsfeed newsfeed)
        {
            CustomTabsIntent customTabsIntent = new CustomTabsIntent.Builder().SetToolbarColor(Color.ParseColor("#002132")).Build();
            customTabsIntent.Intent.AddFlags(ActivityFlags.NoHistory | ActivityFlags.SingleTop | ActivityFlags.NewTask);
            customTabsIntent.Intent.SetData(Uri.Parse(newsfeed.UrlToNewsfeed));
            return PendingIntent.GetActivity(Application.Context, (int)(DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000), customTabsIntent.Intent, PendingIntentFlags.OneShot);
        }
    }
}