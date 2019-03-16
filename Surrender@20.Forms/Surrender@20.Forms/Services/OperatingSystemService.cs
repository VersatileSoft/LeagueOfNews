using LeagueOfNews.Core.Interface;
using Xamarin.Forms;

namespace LeagueOfNews.Forms.Services
{
    public class OperatingSystemService : IOperatingSystemService
    {
        public SystemType GetSystemType()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.UWP: return SystemType.UWP;
                case Device.Android: return SystemType.Android;
                case Device.iOS: return SystemType.iOS;
                case Device.macOS: return SystemType.iOS;
                default: return SystemType.Unsupported;
            }
        }
    }
}