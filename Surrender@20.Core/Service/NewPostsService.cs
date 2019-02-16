using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surrender_20.Core.Service
{
    public class NewPostsService : INewPostsService
    {

        private readonly INotificationService _notificationService;
        private readonly INewsfeedService _newsfeedService;
        private readonly IPersistentDataService _saveDataService;

        public NewPostsService(INotificationService notificationService, INewsfeedService newsfeedService, IPersistentDataService saveDataService)
        {
            _notificationService = notificationService;
            _newsfeedService = newsfeedService;
            _saveDataService = saveDataService;
        }

        public async Task CheckNewPosts()
        {
            await CheckNewPosts(NewsCategory.Official);
            await CheckNewPosts(NewsCategory.SurrenderHome);
        }

        private async Task CheckNewPosts(NewsCategory page)
        {
            List<Newsfeed> list = new List<Newsfeed>(await _newsfeedService.LoadNewsfeedsAsync(page));
            List<Newsfeed> newPosts = new List<Newsfeed>();
            string lastPostTitle = _saveDataService.GetLastPostTitle(page);
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

            _saveDataService.SaveLastPostTitle(page, list[0].Title);

            if (newPosts.Count > 0)
            {
                _notificationService.ShowNewPostNotification(newPosts[0], page); //TODO Show all new post not only one
            }
        }
    }
}