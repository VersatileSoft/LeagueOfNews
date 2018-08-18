using Android.App;
using Android.OS;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using Surrender_20.Core.Interface;
using Surrender_20.Forms.Interfaces;
using Surrender_20.Forms.Services;

namespace Surrender_20.Forms.Droid
{
    [Activity(Label = "Surrender_20.Forms", MainLauncher = true, Theme = "@style/MainTheme", NoHistory = true)]
    public class MainActivity : MvxFormsAppCompatActivity<AndroidSetup, FormsCoreApp, App>
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
        }
    }

    sealed public class AndroidSetup : MvxFormsAndroidSetup<FormsCoreApp, App>
    {
        protected override void InitializeLastChance()
        {
            Mvx.IoCProvider.RegisterSingleton(typeof(IOperatingSystemService), new OperatingSystemService());
            Mvx.IoCProvider.RegisterSingleton(typeof(IMasterDetailService), new MasterDetailService());
            
            base.InitializeLastChance();
        }
    }
}

