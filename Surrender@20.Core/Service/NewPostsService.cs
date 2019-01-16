using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20.Core.Service
{
    public class NewPostsService : INewPostsService
    {

        private const string LAST_POST_KEY_SURRENDER = "LAST_POST_KEY_SURRENDER";
        private const string LAST_POST_KEY_OFFICIAL = "LAST_POST_KEY_OFFICIAL";

        private INotificationService _notificationService;
        private INewsfeedService _newsfeedService;
        private ISaveDataService _saveDataService;

        public string elo { get; set; }

        public NewPostsService(INotificationService notificationService, INewsfeedService newsfeedService, ISaveDataService saveDataService)
        {
            _notificationService = notificationService;
            _newsfeedService = newsfeedService;
            _saveDataService = saveDataService;
        }

        public async Task CheckNewPosts()
        {
            await CheckNewPosts(LAST_POST_KEY_OFFICIAL, Pages.Official);
            await CheckNewPosts(LAST_POST_KEY_SURRENDER, Pages.SurrenderHome);
        }

        private async Task CheckNewPosts(string LastPostKey, Pages page)
        {
            List<Newsfeed> list = new List<Newsfeed>(await _newsfeedService.LoadNewsfeedsAsync(page));
            List<Newsfeed> newPosts = new List<Newsfeed>();
            string lastPostTitle = _saveDataService.GetData(LastPostKey);
            if (lastPostTitle != null)
            {
                foreach (Newsfeed newsfeed in list)
                {
                    if (newsfeed.Title == lastPostTitle)
                    {
                        break;
                    }
                    else
                    {
                        newPosts.Add(newsfeed);
                    }
                }
            }

            _saveDataService.SaveData(LastPostKey, list[0].Title);

            if (newPosts.Count > 0)
                _notificationService.ShowNewPostNotification(newPosts[0]); //TODO Show all new post not only one
        }
    }
}
