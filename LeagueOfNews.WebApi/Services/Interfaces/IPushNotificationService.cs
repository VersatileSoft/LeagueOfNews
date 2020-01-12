
using System.Threading.Tasks;
using LeagueOfNews.Model;

namespace LeagueOfNews.WebApi.Services.Interfaces
{
    public interface IPushNotificationService
    {
        Task PushNotification(Newsfeed notification);
    }
}
