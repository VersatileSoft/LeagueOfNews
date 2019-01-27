using Surrender_20.Model;

namespace Surrender_20.Core.Interface
{
    public interface INotificationService
    {
        void ShowNewPostNotification(Newsfeed newsfeed);

        void RefreshNotificationJobService();

        void CreateNotificationChannel();
    }
}