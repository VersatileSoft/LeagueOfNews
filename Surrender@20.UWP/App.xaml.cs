using MvvmCross;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Platforms.Uap.Views;
using Surrender_20.Core;
using Surrender_20.Core.Interface;
using Surrender_20.Services;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Surrender_20
{
    sealed partial class App : UWPApplication
    {
        public App()
        {
            InitializeComponent();
            Mvx.RegisterSingleton<IOperatingSystemService>(new OperatingSystemService());
        }
    }
    public abstract class UWPApplication : MvxApplication<MvxWindowsSetup<CoreApp>, CoreApp> { }
}
