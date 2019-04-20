using System.Threading.Tasks;

namespace LeagueOfNews.Forms.Interfaces
{
    public interface IChromeCustomTabService
    {
        Task StartChromCustomTab(string url);
    }
}