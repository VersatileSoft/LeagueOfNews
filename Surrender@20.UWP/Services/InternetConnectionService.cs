using Surrender_20.Core.Interface;
using System.Net.NetworkInformation;

namespace Surrender_20.UWP.Services
{
    public class InternetConnectionService : IInternetConnectionService
    {
        public bool IsInternetAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
    }
}