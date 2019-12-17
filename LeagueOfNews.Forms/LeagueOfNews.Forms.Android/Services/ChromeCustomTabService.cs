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
        public Task StartChromeCustomTab(string url)
        {
            return Task.Run(() =>
            {
                CustomTabsIntent customTabsIntent = new CustomTabsIntent.Builder().SetToolbarColor(Color.ParseColor("#202429")).Build();
                customTabsIntent.Intent.AddFlags(ActivityFlags.NoHistory | ActivityFlags.SingleTop | ActivityFlags.NewTask);
                customTabsIntent.LaunchUrl(Application.Context, Uri.Parse(url));
            });
        }
    }
}