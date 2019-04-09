using LeagueOfNews.Core;
using LeagueOfNews.Core.Interface;
using LeagueOfNews.UWP.Services;
using LeagueOfNews.UWP.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.Plugin;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.Storage;
using Windows.UI.ViewManagement;

namespace LeagueOfNews.UWP
{
    public abstract class UWPApplication : MvxApplication<UWPSetup, CoreApp> { }

    public sealed partial class App : UWPApplication
    {
        public App()
        {
            InitializeComponent();

            Core.Interface.ApplicationTheme theme = (ApplicationData.Current.LocalSettings.Values.TryGetValue("Theme", out object value))
                ? (Core.Interface.ApplicationTheme)Enum.Parse(typeof(Core.Interface.ApplicationTheme), value as string)
                : Core.Interface.ApplicationTheme.Default;

            switch (theme)
            {
                case Core.Interface.ApplicationTheme.Light:
                    RequestedTheme = Windows.UI.Xaml.ApplicationTheme.Light;
                    break;

                case Core.Interface.ApplicationTheme.Dark:
                    RequestedTheme = Windows.UI.Xaml.ApplicationTheme.Dark;
                    break;

                case Core.Interface.ApplicationTheme.Default:
                    UISettings uiSettings = new UISettings();
                    string uiTheme = uiSettings.GetColorValue(UIColorType.Background).ToString();

                    if (uiTheme == "#FF000000")
                    {
                        RequestedTheme = Windows.UI.Xaml.ApplicationTheme.Dark;
                    }
                    else if (uiTheme == "#FFFFFFFF")
                    {
                        RequestedTheme = Windows.UI.Xaml.ApplicationTheme.Light;
                    }
                    break;
            }
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