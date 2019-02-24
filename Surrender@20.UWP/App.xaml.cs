using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using Surrender_20.Core;
using Surrender_20.Core.Interface;
using Surrender_20.UWP.Services;
using Surrender_20.UWP.ViewModels;
using System.Collections.Generic;
using System.Reflection;

namespace Surrender_20.UWP
{
    public abstract class UWPApplication : MvxApplication<UWPSetup, CoreApp> { }

    public sealed partial class App : UWPApplication
    {
        public App()
        {
            InitializeComponent();
        }
    }

    public sealed class UWPSetup : MvxWindowsSetup<CoreApp>
    {
        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.RegisterSingleton(typeof(ISettingsService), new SettingsService());
            Mvx.IoCProvider.RegisterSingleton(typeof(IOperatingSystemService), new OperatingSystemService());
            Mvx.IoCProvider.RegisterSingleton(typeof(IInternetConnectionService), new InternetConnectionService());
            Mvx.IoCProvider.RegisterSingleton(typeof(INotificationService), new NotificationService());
        }

        protected override void InitializeLastChance()
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, MvxAppStart<MainPageViewModel>>();
        }

        protected override void InitializeApp(IMvxPluginManager pluginManager, IMvxApplication app)
        {
            base.InitializeApp(pluginManager, app);

            ApplicationTheme theme = Mvx.IoCProvider.Resolve<ISettingsService>().Theme;
            //(app as UWPApplication).RequestedTheme =
            //    (Windows.UI.Xaml.ApplicationTheme)((theme == ApplicationTheme.Default) ? ApplicationTheme.Light : theme);
        }

        public override IEnumerable<Assembly> GetViewModelAssemblies()
        {
            List<Assembly> list = new List<Assembly>();
            list.AddRange(base.GetViewModelAssemblies());
            list.Add(typeof(MainPageViewModel).Assembly);

            return list;
        }
    }
}