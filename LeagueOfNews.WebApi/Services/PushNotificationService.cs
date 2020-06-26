using System;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
using LeagueOfNews.Model;
using LeagueOfNews.Utils;
using LeagueOfNews.WebApi.Services.Interfaces;

namespace LeagueOfNews.WebApi.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        public async Task PushNotification(Newsfeed newsfeed)
        {
            Message message = new Message()
            {
                Data = newsfeed.ToDictionary(),
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
