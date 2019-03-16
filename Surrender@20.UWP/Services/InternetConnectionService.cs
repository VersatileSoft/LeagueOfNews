using LeagueOfNews.Core.Interface;
using System.Net.NetworkInformation;

namespace LeagueOfNews.UWP.Services
{
    public class InternetConnectionService : IInternetConnectionService
    {
        public bool IsInternetAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}