using Android.App;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using Surrender_20.Core;

namespace Surrender_20.Forms.Droid
{
    [Activity(Label = "Surrender_20.Forms", MainLauncher = true, Theme = "@style/MainTheme", NoHistory = true)]
    public class MainActivity : MvxFormsAppCompatActivity<MvxFormsAndroidSetup<CoreApp, FormsUI.App>, CoreApp, FormsUI.App>
    {
    }
}

