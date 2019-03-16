using LeagueOfNews.Core.Interface;

namespace LeagueOfNews.UWP.Services
{
    public class OperatingSystemService : IOperatingSystemService
    {
        public SystemType GetSystemType()
        {
            return SystemType.UWP;
        }
    }
}