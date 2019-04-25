using LeagueOfNews.Core;
using LeagueOfNews.Core.Interface;
using LeagueOfNews.Model;
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
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.Storage;
using Windows.System.Profile;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace LeagueOfNews.UWP
{
    public abstract class UWPApplication : MvxApplication<UWPSetup, CoreApp> { }

    public sealed partial class App : UWPApplication
    {
        public static bool RunningOnDesktop => AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Desktop";
        public static bool RunningOnXbox => AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Xbox";
        public static bool RunningOnMobile => AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile";

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

            if (RunningOnDesktop)
            {
                TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            }
        }

        protected override void OnActivated(IActivatedEventArgs e)
        {
            base.OnActivated(e);

            if (e is ToastNotificationActivatedEventArgs)
            {
                ToastNotificationActivatedEventArgs toastActivationArgs = e as ToastNotificationActivatedEventArgs;

                if (toastActivationArgs.Argument.Length == 0)
                {
                    throw new ArgumentException("No argument value has been provided");
                }
                else
                {
                    var arguments = toastActivationArgs.Argument
                        .Split("&")
                        .Select(query => new
                        {
                            name = query.Split('=')[0],
                            value = query.Split('=')[1]
                        }
                     );

                    switch (arguments.Single(match => match.name == "action").value)
                    {
                        case "show":
                            Frame rootFrame = InitializeFrame(e);

                            NewsfeedItemViewModel itemVM = MvxIoCProvider.Instance.Resolve<NewsfeedItemViewModel>();
                            itemVM.Prepare(new Newsfeed
                            {
                                Title = arguments.Single(match => match.name == "title").value,
                                Date = arguments.Single(match => match.name == "date").value,
                                UrlToNewsfeed = arguments.Single(match => match.name == "url").value
                            });
                            break;

                        default:
                            throw new ArgumentException("No valid argument category has been provided");
                    }
                }
            }
        }

        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            switch (args.TaskInstance.Task.Name)
            {
                case NotificationService.TASK_CHECK_POSTS_NAME:
                    BackgroundTaskDeferral deferral = args.TaskInstance.GetDeferral();

                    MvxWindowsSetupSingleton.EnsureSingletonAvailable(RootFrame);
                    Task.Run(async () =>
                    {
                        await Mvx.IoCProvider.Resolve<INewPostsService>().CheckNewPosts();
                        deferral.Complete();
                    });

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
            Mvx.IoCProvider.Resolve<INotificationService>().RefreshNotificationJobService();
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