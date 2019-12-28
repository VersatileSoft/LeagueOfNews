using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
using LeagueOfNews.Model;
using LeagueOfNews.WebApi.Services.Interfaces;

namespace LeagueOfNews.WebApi.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        public async Task PushNotification(Newsfeed newsfeed)
        {
            Message message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    { "title", newsfeed.Title },
                    { "body", newsfeed.ShortDescription },
                    { "icon", newsfeed.ImageUrl },
                    { "url", newsfeed.UrlToNewsfeed },
                },
                Topic = "News"
            };

            try
            {
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
