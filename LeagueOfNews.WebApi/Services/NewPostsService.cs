using System.Collections.Generic;
using System.Linq;
using System.Timers;
using LeagueOfNews.Model;
using LeagueOfNews.WebApi.Services.Interfaces;
using Microsoft.Extensions.Options;
using ServiceIterator;

namespace LeagueOfNews.WebApi.Services
{
    [ExecuteDelay(Milliseconds = 1000 * 60 * 30)]
    public class NewPostsService : IExecutable
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly INewsfeedService _newsfeedService;
        private readonly AppConfig _appConfig;
        private readonly Dictionary<int, string> _lastPosts;

        public NewPostsService(IPushNotificationService pushNotificationService, IOptions<AppConfig> options, INewsfeedService newsfeedService)
        {
            _lastPosts = new Dictionary<int, string>();
            _pushNotificationService = pushNotificationService;
            _newsfeedService = newsfeedService;
            _appConfig = options.Value;
        }

        public async void Execute(object source, ElapsedEventArgs e)
        {

            /*  Newsfeed lastPost = (await _newsfeedService.GetNewsfeeds(0, 1)).First();
              lastPost.WebsiteName = _appConfig.Websites.First().Name;
              await _pushNotificationService.PushNotification(lastPost);*/

            foreach (Website website in _appConfig.Websites)
            {
                Newsfeed lastPost = (await _newsfeedService.GetNewsfeeds(website.Id, 1)).First();
                if (_lastPosts.TryGetValue(website.Id, out string lastUrl))
                {
                    if (lastPost.UrlToNewsfeed != lastUrl)
                    {
                        _lastPosts[website.Id] = lastPost.UrlToNewsfeed;
                        lastPost.WebsiteName = website.Name;
                        await _pushNotificationService.PushNotification(lastPost);
                    }
                }
                else
                {
                    _lastPosts.Add(website.Id, lastPost.UrlToNewsfeed);
                }
            }
        }
    }
}
