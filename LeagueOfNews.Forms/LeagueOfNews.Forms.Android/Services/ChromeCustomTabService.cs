using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net;
using Android.Support.CustomTabs;
using LeagueOfNews.Forms.Interfaces;
using System.Threading.Tasks;

namespace LeagueOfNews.Forms.Droid.Services
{
    public class ChromeCustomTabService : IChromeCustomTabService
    {
        public Task StartChromCustomTab(string url)
        {
            return Task.Run(() =>
            {
                CustomTabsIntent.Builder builder = new CustomTabsIntent.Builder();
                builder.SetToolbarColor(Color.ParseColor("#002132"));
                CustomTabsIntent customTabsIntent = builder.Build();
                customTabsIntent.Intent.SetFlags(ActivityFlags.NewTask);
                customTabsIntent.LaunchUrl(Application.Context, Uri.Parse(url));
            });
        }
    }
}