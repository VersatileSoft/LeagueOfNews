using Android.App;
using Android.Content;
using Android.Net;
using Android.Widget;
using Surrender_20.Core.Interface;

namespace Surrender_20.Forms.Droid.Services
{
    public class InternetConnectionService : IInternetConnectionService
    {
        public bool CheckInternetConnection()
        {
            ConnectivityManager cm = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            bool isConnected = cm.ActiveNetworkInfo == null ? false : cm.ActiveNetworkInfo.IsConnected;
            if (!isConnected)
            {
                Toast.MakeText(Application.Context, "No internet connection", ToastLength.Short).Show();
            }

            return isConnected;
        }
    }
}