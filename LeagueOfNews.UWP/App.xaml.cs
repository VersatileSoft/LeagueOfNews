using LeagueOfNews.Core;
using LeagueOfNews.Core.Interface;
using LeagueOfNews.Model;
using LeagueOfNews.UWP.Services;
using LeagueOfNews.UWP.View;
using LeagueOfNews.UWP.ViewModels;
using Microsoft.QueryStringDotNET;
using Microsoft.Toolkit.Uwp.Notifications;
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
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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

        protected override void OnActivated(IActivatedEventArgs e)
        {
            base.OnActivated(e);

            // Handle toast activation
            if (e is ToastNotificationActivatedEventArgs)
            {
                var toastActivationArgs = e as ToastNotificationActivatedEventArgs;

                // If empty args, no specific action (just launch the app)
                if (toastActivationArgs.Argument.Length == 0)
                {
                    throw new NotImplementedException();
                }

                // Otherwise an action is provided
                else
                {
                    // Parse the query string
                    QueryString args = QueryString.Parse(toastActivationArgs.Argument);

                    // See what action is being requested 
                    switch (args["action"])
                    {
                        // Open the image
                        case "show":

                            Frame rootFrame = InitializeFrame(e);

                            var url = args["website"] == "Surrender"
                                ? Mvx.IoCProvider.Resolve<ISettingsService>().WebsiteHistoryData.LastOfficialPostUrl
                                : Mvx.IoCProvider.Resolve<ISettingsService>().WebsiteHistoryData.LastOfficialPostUrl;


                            NewsfeedItemViewModel itemVM = MvxIoCProvider.Instance.Resolve<NewsfeedItemViewModel>();
                            itemVM.Prepare(new Newsfeed { Title = args["title"], Date = args["date"], UrlToNewsfeed = args["url"] });

                            // Otherwise navigate to view it
                            //rootFrame.Navigate(typeof(NewsfeedItemView), imageUrl);
                            break;


                        // Open the conversation
                        case "browser":

                            Task.Run(async () =>
                            {
                                var uri = new Uri(args["url"]);
                                var success = await Windows.System.Launcher.LaunchUriAsync(uri);
                            });
                            break;

                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }

        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            switch (args.TaskInstance.Task.Name)
            {
                case NotificationService.TASK_NAME:
                    var deferral = args.TaskInstance.GetDeferral();
                    MvxWindowsSetupSingleton.EnsureSingletonAvailable(RootFrame);

                    Task.Run(async () =>
                    {

                        await Mvx.IoCProvider.Resolve<INewPostsService>().CheckNewPosts();
                        deferral.Complete();
                    });
                    
                    break;
            }

            //deferral.Complete();
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