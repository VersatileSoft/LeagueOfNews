using System.Threading.Tasks;
using LeagueOfNews.Model;

namespace LeagueOfNewsNew.XF.Services.Interfaces
{
    public interface IAppConfigService
    {
        Task LoadConfig();
        AppConfig AppConfig { get; set; }
    }
}
