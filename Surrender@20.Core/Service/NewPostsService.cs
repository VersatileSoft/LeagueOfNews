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
        private INotificationService _notificationService;
        private INewsfeedService _newsfeedService;

        public string elo { get; set; }

        public NewPostsService(INotificationService notificationService, INewsfeedService newsfeedService)
        {
            _notificationService = notificationService;
            _newsfeedService = newsfeedService;
        }

        public async Task CheckNewPosts()
        {
           List<Newsfeed> list = new List<Newsfeed>(await _newsfeedService.LoadNewsfeedsAsync(Pages.SurrenderHome));

            _notificationService.ShowNewPostNotification(list[0]);          
        }
    }
}
