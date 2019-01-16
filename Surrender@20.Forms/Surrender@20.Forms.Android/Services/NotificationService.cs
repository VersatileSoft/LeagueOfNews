using Android.App;
using Android.Content;
using Android.Support.V4.App;
using MvvmCross;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using Surrender_20.Forms.ViewModels;
using Surrender_20.Model;
using Application = Android.App.Application;

namespace Surrender_20.Forms.Droid.Services
{

    public class NotificationService : INotificationService
    {

        public void ShowNewPostNotification(Newsfeed newsfeed)
        {
            var notification = new NotificationCompat.Builder(Application.Context, MainActivity.CHANNEL_ID)
                .SetContentTitle(newsfeed.Title)
                .SetContentText(newsfeed.ShortDescription)
                .SetSmallIcon(Resource.Drawable.AppIcon)
                .SetContentIntent(GetContentIntent(newsfeed))
              
                .SetShowWhen(true)
                .SetAutoCancel(true)
                .Build();

            var notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(1000, notification);

        }

        private PendingIntent GetContentIntent(Newsfeed newsfeed)
        {
            var bundle = new MvxBundle();
            bundle.Write(newsfeed);

            var request = new MvxViewModelRequest<NewsfeedItemViewModel>(bundle, null);

            var converter = Mvx.IoCProvider.Resolve<IMvxNavigationSerializer>();
            var requestText = converter.Serializer.SerializeObject(request);

            var intent = new Intent(Application.Context, typeof(MainActivity));

            // We only want one activity started
            intent.AddFlags(flags: ActivityFlags.SingleTop);

            intent.PutExtra("MvxLaunchData", requestText);

            // Create Pending intent, with OneShot. We're not going to want to update this.
            return PendingIntent.GetActivity(Application.Context, 0, intent, PendingIntentFlags.OneShot);
        }
    }
}