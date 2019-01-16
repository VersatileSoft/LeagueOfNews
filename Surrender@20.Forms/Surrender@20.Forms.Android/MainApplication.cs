using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using LabelHtml.Forms.Plugin.Droid;
using MvvmCross;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.IoC;
using MvvmCross.Platform.Droid.Platform;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;
using Surrender_20.Core;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Forms.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Surrender_20.Forms.Droid
{

    [Application]
    public class MainApplication : MvxAndroidApplication<AndroidSetup, CoreApp>
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }

    sealed public class AndroidSetup : MvxAndroidSetup<CoreApp>
    {
        protected override void InitializeLastChance()
        {
            // Mvx.IoCProvider.RegisterSingleton(typeof(IOperatingSystemService), new OperatingSystemService()); //TODO move to InitializeFirstChance
            //Mvx.IoCProvider.RegisterSingleton(typeof(IMasterDetailService), new MasterDetailService());       //TODO same /\
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, MvxAppStart<MainViewModel>>();

            base.InitializeLastChance(); //TODO remove (check if work)
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxAppCompatViewPresenter(AndroidViewAssemblies);
            Mvx.IoCProvider.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);

            Mvx.IoCProvider.RegisterSingleton<MvxPresentationHint>(new MvxPanelPopToRootPresentationHint());
            return mvxFragmentsPresenter;
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(Android.Support.Design.Widget.NavigationView).Assembly,
            typeof(Android.Support.Design.Widget.FloatingActionButton).Assembly,
            typeof(Android.Support.V7.Widget.Toolbar).Assembly,
            typeof(Android.Support.V4.Widget.DrawerLayout).Assembly,
            typeof(Android.Support.V4.View.ViewPager).Assembly,
            typeof(MvvmCross.Droid.Support.V7.RecyclerView.MvxRecyclerView).Assembly
        };

        //public override IEnumerable<Assembly> GetViewModelAssemblies()
        //{
        //    var list = new List<Assembly>();
        //    list.AddRange(base.GetViewModelAssemblies());
        //    // list.Add(typeof(NewsfeedItemViewModel).Assembly);
        //    return list.ToArray();
        //}
    }
}

