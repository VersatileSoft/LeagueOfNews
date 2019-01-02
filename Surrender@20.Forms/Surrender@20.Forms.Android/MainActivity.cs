using Android.App;
using Android.OS;
using LabelHtml.Forms.Plugin.Droid;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Surrender_20.Core;
using Surrender_20.Core.Interface;
using Surrender_20.Forms.Interfaces;
using Surrender_20.Forms.Services;
using Surrender_20.Forms.ViewModels;
using System.Collections.Generic;
using System.Reflection;

namespace Surrender_20.Forms.Droid
{
    [Activity(Label = "SurrenderAt20", MainLauncher = true, Theme = "@style/MainTheme", NoHistory = false)]
    public class MainActivity : MvxFormsAppCompatActivity<AndroidSetup, CoreApp, App>
    {
        protected override void OnCreate(Bundle bundle)
        {
            HtmlLabelRenderer.Initialize();
            Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
        }
    }

    sealed public class AndroidSetup : MvxFormsAndroidSetup<CoreApp, App>
    {
        protected override void InitializeLastChance()
        {
            Mvx.IoCProvider.RegisterSingleton(typeof(IOperatingSystemService), new OperatingSystemService()); //TODO move to InitializeFirstChance
            Mvx.IoCProvider.RegisterSingleton(typeof(IMasterDetailService), new MasterDetailService());       //TODO same /\
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, MvxAppStart<RootPageViewModel>>();

            base.InitializeLastChance(); //TODO remove (check if work)
        }

        public override IEnumerable<Assembly> GetViewModelAssemblies()
        {
            var list = new List<Assembly>();
            list.AddRange(base.GetViewModelAssemblies());
            list.Add(typeof(NewsfeedItemViewModel).Assembly);
            return list.ToArray();
        }
    }
}

