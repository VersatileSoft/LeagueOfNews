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

            //BackgroundTaskRegistration.AllTasks.Single(i => i.Value.Name.Equals(TASK_NAME))
            //    .Value.Unregister(true);

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
            ToastNotificationManager.CreateToastNotifier()
                .Show(new ToastNotification(GenerateNotifcationBody(newsfeed).GetXml()));
        }

        private ToastContent GenerateNotifcationBody(Newsfeed Newsfeed)
        {
            var description = Newsfeed.ShortDescription.Length > 120
                ? Newsfeed.ShortDescription.Substring(0, 120) + "..."
                : Newsfeed.ShortDescription;

            var parameters = "action=show?"
                + "title=" + Newsfeed.Title + "&"
                + "date=" + Newsfeed.Date + "&"
                + "url=" + Newsfeed.UrlToNewsfeed + "&"
                + "website=" + Newsfeed.Website.ToString();

            var browserParameters = "action=show?"
                + "title=" + Newsfeed.Title + "&"
                + "date=" + Newsfeed.Date + "&"
                + "url=" + Newsfeed.UrlToNewsfeed + "&"
                + "website=" + Newsfeed.Website.ToString();

            return new ToastContent()
            {
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = Newsfeed.Title
                            },
                            new AdaptiveText()
                            {
                                Text = description
                            }
                        },
                        HeroImage = new ToastGenericHeroImage()
                        {
                            Source = Newsfeed.UrlToNewsfeed
                        },
                        Attribution = new ToastGenericAttributionText()
                        {
                            Text = "League of News • League of Legends official"
                        }
                    }
                },
                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        new ToastButton("Read in app", parameters)
                        {
                            ActivationType = ToastActivationType.Background,
                            ImageUri = @"C:\Users\qvaris\Desktop\InApp.png"
                        },
                        new ToastButton("Open in browser", browserParameters)
                        {
                            ActivationType = ToastActivationType.Background,
                            ImageUri = @"C:\Users\qvaris\Desktop\InBrowser.png"
                        }
                    }
                },
                Launch = parameters,
                Duration = ToastDuration.Long
            };
        }
    }
}