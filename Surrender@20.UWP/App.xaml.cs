using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using Surrender_20.Core;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.UWP.Services;
using Surrender_20.UWP.ViewModels;

namespace Surrender_20.UWP
{
    sealed partial class App : UWPApplication
    {
        public App()
        {
            InitializeComponent();
        }
    }

    sealed public class UWPSetup : MvxWindowsSetup<CoreApp>
    {
        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.RegisterSingleton(typeof(IOperatingSystemService), new OperatingSystemService());
        }

        protected override void InitializeLastChance()
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, MvxAppStart<MainPageViewModel>>();
        }
    }

    public abstract class UWPApplication : MvxApplication<UWPSetup, CoreApp> { }
}
