using LeagueOfNews.Core.Interface;
using LeagueOfNews.Model;
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
using MvvmCross;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace LeagueOfNews.UWP.Services
{
    public class NotificationService : INotificationService
    {
        public const string TASK_NAME = "Backgrou2ndPostTask1";

        public NotificationService()
        {
            CreateNotificationChannel();
        }

        public void CreateNotificationChannel()
        {
            var settings = Mvx.IoCProvider.Resolve<ISettingsService>();

            if (!settings.HasNotificationsEnabled || settings.NewPostCheckFrequency == -1)
                return;

            BackgroundTaskRegistration.AllTasks.Single(i => i.Value.Name.Equals(TASK_NAME))
                .Value.Unregister(true);

            // If background task is already registered, do nothing
            if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(TASK_NAME)))
                return;

            var trigger = new ApplicationTrigger();
            var builder = new BackgroundTaskBuilder();
            builder.Name = TASK_NAME;
            builder.CancelOnConditionLoss = false;
            builder.SetTrigger(trigger);
            //builder.SetTrigger(new TimeTrigger((uint) settings.NewPostCheckFrequency, false));
            builder.Register();
            Task.Run(async () =>
            {
                await trigger.RequestAsync();
            });
        }

        public void RefreshNotificationJobService()
        {
            if (Mvx.IoCProvider.Resolve<ISettingsService>().HasNotificationsEnabled)
            {
                if (!BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(TASK_NAME)))
                {
                    CreateNotificationChannel();
                }
            }
            else
            {
                BackgroundTaskRegistration.AllTasks.Single(i => i.Value.Name.Equals(TASK_NAME))
                    .Value.Unregister(true);
            }
        }

        public void ShowNewPostNotification(Newsfeed newsfeed, NewsWebsite page)
        {
            var description = newsfeed.ShortDescription.Length > 120
                ? newsfeed.ShortDescription.Substring(0, 120) + "..."
                : newsfeed.ShortDescription;

            /*
            var parameters = new QueryString {
                { "action", "show" },
                { "title", newsfeed.Title },
                { "date", newsfeed.Date },
                { "url", newsfeed.UrlToNewsfeed },
                { "website", newsfeed.Website.ToString() }
            }.ToString();

            var browserParameters = new QueryString {
                { "action", "show" },
                { "title", newsfeed.Title },
                { "date", newsfeed.Date },
                { "url", newsfeed.UrlToNewsfeed },
                { "website", newsfeed.Website.ToString() }
            }.ToString();
            */

            var parameters = "action=show?"
                + "title=" + newsfeed.Title + "&"
                + "date=" + newsfeed.Date + "&"
                + "url=" + newsfeed.UrlToNewsfeed + "&"
                + "website=" + newsfeed.Website.ToString();

            var browserParameters = "action=show?"
                + "title=" + newsfeed.Title + "&"
                + "date=" + newsfeed.Date + "&"
                + "url=" + newsfeed.UrlToNewsfeed + "&"
                + "website=" + newsfeed.Website.ToString();

            var text = (await FileIO.ReadTextAsync(file))
                .Replace("{title}", newsfeed.Title)
                .Replace("{image}", newsfeed.ImageUri)
                .Replace("{description}", description)
                .Replace("{parameters}", parameters)
                .Replace("{browserParameters}", browserParameters);

            Task.Run(async () =>
            {
                var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Views/Misc/ToastNotification.xml"));


                XmlDocument document = new XmlDocument();
                try
                {
                    document.LoadXml(text);
                    ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(document));

                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            });

        }
    }
}