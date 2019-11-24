using LeagueOfNews.Core.Interface;
using Xamarin.Forms;

namespace LeagueOfNews.Forms.Services
{
    public class OperatingSystemService : IOperatingSystemService
    {
        public SystemType GetSystemType()
        {
            return Device.RuntimePlatform switch
            {
                Device.UWP => SystemType.UWP,
                Device.Android => SystemType.Android,
                Device.iOS => SystemType.iOS,
                Device.macOS => SystemType.iOS,
                _ => SystemType.Unsupported,
            };
        }
    }
}