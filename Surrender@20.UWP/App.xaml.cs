using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using Surrender_20.Core;
using Surrender_20.Core.Interface;
using Surrender_20.UWP.Services;
using Surrender_20.UWP.ViewModels;
using System.Collections.Generic;
using System.Reflection;

namespace Surrender_20.UWP
{
    public sealed partial class App : UWPApplication
    {
        public App()
        {
            //MainPageViewModel ViewModel = new MainPageViewModel();

            //ViewModel.LoadSettings();
            //RequestedTheme = MainPageViewModel.SelectedTheme;

            InitializeComponent();
        }
    }

    public sealed class UWPSetup : MvxWindowsSetup<CoreApp>
    {
        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.RegisterSingleton(typeof(IOperatingSystemService), new OperatingSystemService());
            Mvx.IoCProvider.RegisterSingleton(typeof(IInternetConnectionService), new InternetConnectionService());
            Mvx.IoCProvider.RegisterSingleton(typeof(INotificationService), new NotificationService());
            Mvx.IoCProvider.RegisterSingleton(typeof(ISaveDataService), new SaveDataService());
        }

        protected override void InitializeLastChance()
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, MvxAppStart<MainPageViewModel>>();
        }

        public override IEnumerable<Assembly> GetViewModelAssemblies()
        {
            List<Assembly> list = new List<Assembly>();
            list.AddRange(base.GetViewModelAssemblies());
            list.Add(typeof(MainPageViewModel).Assembly);
            return list.ToArray();
        }
    }

    public abstract class UWPApplication : MvxApplication<UWPSetup, CoreApp> { }
}