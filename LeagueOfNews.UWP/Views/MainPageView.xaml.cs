using LeagueOfNews.UWP.ViewModels;
using LeagueOfNews.UWP.Views.MessageBoxes;
using MvvmCross.Platforms.Uap.Views;
using System;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace LeagueOfNews.UWP.Views
{
    public sealed partial class MainPageView : MvxWindowsPage
    {
        private BitmapImage LogoLight, LogoDark, ItemLogoLight, ItemLogoDark;
        private readonly ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

        private readonly ConnectionDialog ConnectionDialog = new ConnectionDialog();

        public MainPageView()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            //Load images
            LogoLight = new BitmapImage(new Uri("ms-appx:///Assets/TitleBarAssets/AppLogoWhite.png"));
            LogoDark = new BitmapImage(new Uri("ms-appx:///Assets/TitleBarAssets/AppLogoDark.png"));

            ItemLogoLight = new BitmapImage(new Uri("ms-appx:///Assets/TitleBarAssets/PlaceholderWhite.png"));
            ItemLogoDark = new BitmapImage(new Uri("ms-appx:///Assets/TitleBarAssets/PlaceholderDark.png"));

            //Change appearance to proper theme
            ChangeThemeLogo();

            if (App.RunningOnDesktop)
            {
                //Customize titlebar
                CoreApplicationViewTitleBar CoreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                CoreTitleBar.ExtendViewIntoTitleBar = true;
                ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();

            //Run check for internet connection
            ConnectionDialog.Execute(
                () => (ViewModel as MainPageViewModel).CheckInternetConnection());
        }


        private void ChangeThemeLogo()
        {
            switch (Application.Current.RequestedTheme)
            {
                case ApplicationTheme.Light:
                    LogoImage.Source = LogoDark;
                    ItemLogo.Source = ItemLogoDark;
                    titleBar.ButtonForegroundColor = Colors.Black;
                    break;

                case ApplicationTheme.Dark:
                    LogoImage.Source = LogoLight;
                    ItemLogo.Source = ItemLogoLight;
                    titleBar.ButtonForegroundColor = Colors.White;
                    break;
            }
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

            ApplicationView applicationView = ApplicationView.GetForCurrentView();
            if (applicationView.IsFullScreenMode == true)
            {
                MasterColumn.Width = new GridLength(0);
                MasterView.Visibility = Visibility.Collapsed;
                DragArea.Visibility = Visibility.Collapsed;
                NavigationBar.IsPaneVisible = false;
            }
            else if (applicationView.IsFullScreenMode == false)
            {
                MasterColumn.Width = new GridLength(450);
                MasterView.Visibility = Visibility.Visible;
                DragArea.Visibility = Visibility.Visible;
                NavigationBar.IsPaneVisible = true;
            }
        }

        private void MvxWindowsPage_GotFocus(object sender, RoutedEventArgs e)
        {
            ChangeThemeLogo();
        }

        private void Settings_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            (ViewModel as MainPageViewModel).SelectWebsiteCommand.Execute(null);
        }
    }
}