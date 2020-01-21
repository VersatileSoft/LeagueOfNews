using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using AndroidX.Browser.CustomTabs;
using AndroidX.Core.App;
using Firebase.Messaging;
using LeagueOfNews.Model;
using LeagueOfNews.Utils;
using Uri = Android.Net.Uri;

namespace LeagueOfNewsNew.XF.Droid.Services
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class AndroidFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message) => ShowNewPostNotification(message.Data.FromDictionary<Newsfeed>());

        public void ShowNewPostNotification(Newsfeed newsfeed)
        {
            Notification notification = new NotificationCompat.Builder(Application.Context, MainActivity.CHANNEL_ID)
                .SetContentTitle(newsfeed.Title)
                .SetContentText(newsfeed.ShortDescription)
                .SetSmallIcon(Resource.Drawable.NotificationIcon)
                .SetContentIntent(GetContentIntent(newsfeed))
                .SetShowWhen(true)
                .SetAutoCancel(true)
                .SetGroup(newsfeed.WebsiteName)
                .Build();

            NotificationManagerCompat notificationManager = NotificationManagerCompat.From(Application.Context);

            notificationManager.Notify((int)(DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000), notification);
        }

        private PendingIntent GetContentIntent(Newsfeed newsfeed)
        {
            CustomTabsIntent customTabsIntent = new CustomTabsIntent.Builder().SetToolbarColor(Color.ParseColor("#202429")).Build();
            customTabsIntent.Intent.AddFlags(ActivityFlags.NoHistory | ActivityFlags.SingleTop | ActivityFlags.NewTask);
            customTabsIntent.Intent.SetData(Uri.Parse(newsfeed.UrlToNewsfeed));
            return PendingIntent.GetActivity(Application.Context, (int)(DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000), customTabsIntent.Intent, PendingIntentFlags.OneShot);
        }
    }
}