using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using LeagueOfNews.Core;
using LeagueOfNews.Core.Interface;
using LeagueOfNews.Forms.Droid.Services;
using LeagueOfNews.Forms.Interfaces;
using LeagueOfNews.Forms.Services;
using LeagueOfNews.Forms.ViewModels;
using System.Collections.Generic;
using System.Reflection;

namespace LeagueOfNews.Forms.Droid
{
    public sealed class Setup : MvxFormsAndroidSetup<CoreApp, App>
    {
        protected override void InitializeLastChance()
        {
            Mvx.IoCProvider.RegisterSingleton(typeof(IOperatingSystemService), new OperatingSystemService()); 
            Mvx.IoCProvider.RegisterSingleton(typeof(IMasterDetailService), new MasterDetailService());      
            Mvx.IoCProvider.RegisterSingleton(typeof(ITabsInitService), new TabsInitService());      
            Mvx.IoCProvider.RegisterSingleton(typeof(INotificationService), new NotificationService());       
            Mvx.IoCProvider.RegisterSingleton(typeof(ISettingsService), new AndroidSettingsService());       
            Mvx.IoCProvider.RegisterSingleton(typeof(IInternetConnectionService), new InternetConnectionService());       
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, MvxAppStart<RootPageViewModel>>();

            base.InitializeLastChance();
        }

        public override IEnumerable<Assembly> GetViewModelAssemblies()
        {
            List<Assembly> list = new List<Assembly>();
            list.AddRange(base.GetViewModelAssemblies());
            list.Add(typeof(NewsfeedItemViewModel).Assembly);
            return list.ToArray();
        }
    }
}