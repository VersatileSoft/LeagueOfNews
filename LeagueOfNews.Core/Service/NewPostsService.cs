using LeagueOfNews.Core.Interface;
using LeagueOfNews.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeagueOfNews.Core.Service
{
    public class NewPostsService : INewPostsService
    {
        private readonly INotificationService _notificationService;
        private readonly INewsfeedService _newsfeedService;
        private readonly ISettingsService _settingsService;

        public NewPostsService(INotificationService notificationService, INewsfeedService newsfeedService, ISettingsService settingsService)
        {
            _notificationService = notificationService;
            _newsfeedService = newsfeedService;
            _settingsService = settingsService;
        }

        public async Task CheckNewPosts()
        {
            await CheckNewPosts(NewsWebsite.Surrender);
            await CheckNewPosts(NewsWebsite.LoL);
        }

        private async Task CheckNewPosts(NewsWebsite page)
        {
            List<Newsfeed> list = null;
            List<Newsfeed> newPosts = new List<Newsfeed>();
            string lastPostUrl = "";
            switch (page)
            {
                case NewsWebsite.LoL:
                    lastPostUrl = _settingsService.WebsiteHistoryData.LastOfficialPostUrl;
                    list = new List<Newsfeed>(await _newsfeedService.LoadNewsfeedsAsync(NewsCategory.Official));
                    break;
                case NewsWebsite.Surrender:
                    lastPostUrl = _settingsService.WebsiteHistoryData.LastSurrenderPostUrl;
                    list = new List<Newsfeed>(await _newsfeedService.LoadNewsfeedsAsync(NewsCategory.SurrenderHome));
                    break;
                default: break;
            }

            if (!string.IsNullOrWhiteSpace(lastPostUrl))
            {
                foreach (Newsfeed newsfeed in list)
                {
                    if (newsfeed.UrlToNewsfeed == lastPostUrl)
                    {
                        break;
                    }
                    else
                    {
                        newPosts.Add(newsfeed);
                    }
                }
            }

            switch (page)
            {
                case NewsWebsite.LoL:
                    _settingsService.WebsiteHistoryData.LastOfficialPostUrl = list[0].UrlToNewsfeed;
                    break;
                case NewsWebsite.Surrender:
                    _settingsService.WebsiteHistoryData.LastSurrenderPostUrl = list[0].UrlToNewsfeed;
                    break;
                default: break;
            }

            if (newPosts.Count > 0)
            {
                _notificationService.ShowNewPostNotification(newPosts[0], page); //TODO Show all new post not only one
            }
        }
    }
}