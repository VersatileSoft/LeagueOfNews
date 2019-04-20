using LeagueOfNews.Core.Interface;
using LeagueOfNews.Model;
using Microsoft.Toolkit.Uwp.Notifications;
using MvvmCross;
using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace LeagueOfNews.UWP.Services
{
    public class NotificationService : INotificationService
    {
        public const string TASK_CHECK_POSTS_NAME = "BackgroundCheckPostsTask";

        public NotificationService()
        {
            CreateNotificationChannel();
        }

        public void CreateNotificationChannel()
        {
            ISettingsService settings = Mvx.IoCProvider.Resolve<ISettingsService>();

            // If background task is already registered, do nothing
            if (BackgroundTaskRegistration.AllTasks.Any(i => i.Value.Name.Equals(TASK_CHECK_POSTS_NAME)))
            {
                return;
            }

            BackgroundTaskBuilder builder = new BackgroundTaskBuilder
            {
                Name = TASK_CHECK_POSTS_NAME,
                CancelOnConditionLoss = false //TODO check
            };
            builder.SetTrigger(new TimeTrigger((uint)settings.NewPostCheckFrequency, false));
            builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
            builder.Register();
        }

        public void RefreshNotificationJobService()
        {
            ISettingsService settings = Mvx.IoCProvider.Resolve<ISettingsService>();
            if (settings.HasNotificationsEnabled && settings.NewPostCheckFrequency != -1)
            {
                CreateNotificationChannel();
            }
            else
            {
                BackgroundTaskRegistration.AllTasks.Single(i => i.Value.Name.Equals(TASK_CHECK_POSTS_NAME))
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
            string description = Newsfeed.ShortDescription.Length > 120
                ? Newsfeed.ShortDescription.Substring(0, 120) + "..."
                : Newsfeed.ShortDescription;

            string parameters = "action=show&"
                + "title=" + Newsfeed.Title + "&"
                + "date=" + Newsfeed.Date + "&"
                + "url=" + Newsfeed.UrlToNewsfeed + "&"
                + "website=" + Newsfeed.Website.ToString();

            string website = Newsfeed.Website == NewsWebsite.Surrender
                ? "Surrender@20"
                : "League of Legends official";

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
                            Source = Newsfeed.ImageUri
                        },
                        Attribution = new ToastGenericAttributionText()
                        {
                            Text = "League of News • " + website
                        }
                    }
                },
                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        new ToastButton("Read in app", parameters)
                        {
                            ActivationType = ToastActivationType.Foreground,

                            ImageUri = @"ms-appx:///Assets/Notifications/InApp.png"
                        },
                        new ToastButton("Open in browser", Newsfeed.UrlToNewsfeed)
                        {
                            ActivationType = ToastActivationType.Protocol,
                            ImageUri = @"ms-appx:///Assets/Notifications/InBrowser.png"
                        }
                    }
                },
                Launch = parameters,
                ActivationType = ToastActivationType.Foreground
            };
        }
    }
}