using Android.App;
using Android.OS;
using Android.Widget;
using LeagueOfNews.Core.Interface;
using Xamarin.Essentials;

namespace LeagueOfNews.Forms.Droid.Services
{
    public class InternetConnectionService : IInternetConnectionService
    {
        public bool IsInternetAvailable()
        {
            NetworkAccess currentAccess = Connectivity.NetworkAccess;
            bool isConnected = currentAccess == NetworkAccess.Internet ? true : false;
            if (!isConnected)
            {
                try
                {
                    Looper.Prepare();
                    Toast.MakeText(Application.Context, "No internet connection", ToastLength.Short).Show();
                    Looper.Loop();
                }
                catch { }
            }

            return isConnected;
        }
    }
}