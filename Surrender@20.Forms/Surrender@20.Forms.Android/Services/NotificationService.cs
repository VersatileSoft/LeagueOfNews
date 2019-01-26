﻿using Android.App;
using Android.App.Job;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Java.Lang;
using MvvmCross;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using Surrender_20.Forms.Services;
using Surrender_20.Forms.ViewModels;
using Surrender_20.Model;
using Application = Android.App.Application;

namespace Surrender_20.Forms.Droid.Services
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

        public void ShowNewPostNotification(Newsfeed newsfeed)
        {
            Notification notification = new NotificationCompat.Builder(Application.Context, CHANNEL_ID)
                .SetContentTitle(newsfeed.Title)
                .SetContentText(newsfeed.ShortDescription)
                .SetSmallIcon(Resource.Drawable.AppIcon)
                .SetContentIntent(GetContentIntent(newsfeed))

                .SetShowWhen(true)
                .SetAutoCancel(true)
                .Build();

            NotificationManagerCompat notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(1000, notification);            
        }

        public void StartNotificationJobService(int HoursFrequency)
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

        private PendingIntent GetContentIntent(Newsfeed newsfeed)
        {
            MvxBundle bundle = new MvxBundle();
            bundle.Write(newsfeed);

            MvxViewModelRequest<NewsfeedItemViewModel> request = new MvxViewModelRequest<NewsfeedItemViewModel>(bundle, null);

            IMvxNavigationSerializer converter = Mvx.IoCProvider.Resolve<IMvxNavigationSerializer>();
            string requestText = converter.Serializer.SerializeObject(request);

            Intent intent = new Intent(Application.Context, typeof(MainActivity));

            // We only want one activity started
            intent.AddFlags(flags: ActivityFlags.SingleTop);

            intent.PutExtra("MvxLaunchData", requestText);

            // Create Pending intent, with OneShot. We're not going to want to update this.
            return PendingIntent.GetActivity(Application.Context, 0, intent, PendingIntentFlags.OneShot);
        }
    }
}