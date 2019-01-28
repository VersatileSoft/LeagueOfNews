using Surrender_20.Model;

namespace Surrender_20.Core.Interface
{
    public interface INotificationService
    {
        void ShowNewPostNotification(Newsfeed newsfeed, Pages page);

        void RefreshNotificationJobService();

        void CreateNotificationChannel();
    }
}