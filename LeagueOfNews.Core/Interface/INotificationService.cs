using LeagueOfNews.Model;

namespace LeagueOfNews.Core.Interface
{
    public interface INotificationService
    {
        void ShowNewPostNotification(Newsfeed newsfeed, NewsWebsite page);
        void RefreshNotificationJobService();
        void CreateNotificationChannel();
    }
}