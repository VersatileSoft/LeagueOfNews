using Android.App;
using Android.Net;
using Android.Support.CustomTabs;
using LeagueOfNews.Forms.Interfaces;

namespace LeagueOfNews.Forms.Droid.Services
{
    public class ChromeCustomTabService : IChromeCustomTabService
    {
        public void StartChromCustomTab(string url)
        {
            CustomTabsIntent.Builder builder = new CustomTabsIntent.Builder();
            CustomTabsIntent customTabsIntent = builder.Build();
            customTabsIntent.LaunchUrl(Application.Context, Uri.Parse(url));
        }
    }
}