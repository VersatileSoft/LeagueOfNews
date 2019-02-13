using MvvmCross.Base;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using Surrender_20.UWP.Views.MessageBoxes;
using System;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace Surrender_20.UWP.View
{
    public sealed partial class MainPageView : MvxWindowsPage
    {
        private BitmapImage LogoLight, LogoDark;
        private readonly ConnectionDialog ConnectionDialog = new ConnectionDialog();

        private IMvxInteraction<Func<bool>> _checkInternetConnectionInteraction;
        public IMvxInteraction<Func<bool>> CheckInternetConnectionInteraction
        {
            get => _checkInternetConnectionInteraction;
            set
            {
                if (_checkInternetConnectionInteraction != null)
                    _checkInternetConnectionInteraction.Requested -= OnInternetCheckRequested;

                _checkInternetConnectionInteraction = value;
                _checkInternetConnectionInteraction.Requested += OnInternetCheckRequested;
            }
        }

        public MainPageView()
        {
            InitializeComponent();

            LoadImages(); //TODO move all of this properties adjustments into a single f called InitializeView()
            ChangeThemeLogo();

            CoreApplicationViewTitleBar CoreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            CoreTitleBar.ExtendViewIntoTitleBar = true;

            Window.Current.SetTitleBar(DragArea);

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        private void OnInternetCheckRequested(object sender, MvxValueEventArgs<Func<bool>> e)
        {
            ConnectionDialog.Execute(e.Value);
        }

        private void LoadImages()
        {
            LogoLight = new BitmapImage(new Uri("ms-appx:///Assets/Square44x44Logo.scale-100.png"));
            LogoDark = new BitmapImage(new Uri("ms-appx:///Assets/Square44x44Logo.scale-100Dark.png"));
        }

        private void NavigationBar_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationBar.SelectedItem = NavigationBar.MenuItems[0];
        }

        private void MvxWindowsPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            switch (UIViewSettings.GetForCurrentView().UserInteractionMode)
            {
                case UserInteractionMode.Mouse:
                    VisualStateManager.GoToState(this, "MouseLayout", true);
                    DragArea.Visibility = Visibility.Visible;
                    break;

                case UserInteractionMode.Touch:
                default:
                    VisualStateManager.GoToState(this, "TouchLayout", true);
                    DragArea.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void MvxWindowsPage_GotFocus(object sender, RoutedEventArgs e)
        {
            ChangeThemeLogo();
        }

        private void Switch_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void Settings_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private async void AndroidApp_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _ = await Launcher.LaunchUriAsync(new Uri(@"https://play.google.com/store/apps/details?id=com.versatilesofware"));
        }

        private void ChangeThemeLogo()
        {
            if (Application.Current.RequestedTheme == ApplicationTheme.Dark)
            {
                LogoImage.Source = LogoLight;
            }

            else
            {
                LogoImage.Source = LogoDark;
            }
        }
    }
}