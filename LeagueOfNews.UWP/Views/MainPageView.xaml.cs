using LeagueOfNews.UWP.ViewModels;
using LeagueOfNews.UWP.Views.MessageBoxes;
using MvvmCross.Platforms.Uap.Views;
using System;
using Windows.ApplicationModel.Core;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace LeagueOfNews.UWP.View
{
    public sealed partial class MainPageView : MvxWindowsPage
    {
        private BitmapImage LogoLight, LogoDark, ItemLogoLight, ItemLogoDark;
        private readonly ConnectionDialog ConnectionDialog = new ConnectionDialog();

        public MainPageView()
        {
            InitializeComponent();

            Initialize();
        }

        private void Initialize()
        {
            //Load images
            LogoLight = new BitmapImage(new Uri("ms-appx:///Assets/Square44x44Logo.altform-unplated_targetsize-48.png"));
            LogoDark = new BitmapImage(new Uri("ms-appx:///Assets/Square44x44Logo.altform-unplated_targetsize-48Dark.png"));

            ItemLogoLight = new BitmapImage(new Uri("ms-appx:///Assets/Square44x44Logo.altform-unplated_targetsize-256.png"));
            ItemLogoDark = new BitmapImage(new Uri("ms-appx:///Assets/Square44x44Logo.altform-unplated_targetsize-256Dark.png"));

            //Change appearance to proper theme
            ChangeThemeLogo();

            CoreApplicationViewTitleBar CoreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            CoreTitleBar.ExtendViewIntoTitleBar = true;

            Window.Current.SetTitleBar(DragArea);

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            //Run check for internet connection
            //ConnectionDialog.Execute(
            //    () => (ViewModel as MainPageViewModel).CheckInternetConnection()); // To wypierdala na starcie + ItemView sie nie wyświetla, nie działa zmiana strony s@20/oficialny. KOCHAM CIE MARUŚ <3


            /*             LET'S
             *             MAKE
                           UWP
                           GREAT
                           AGAIN             */

            SiteCombo.SelectedItem = "Surrender@20";
        }

        private void ChangeThemeLogo()
        {
            switch (Application.Current.RequestedTheme)
            {
                case ApplicationTheme.Light:
                    LogoImage.Source = LogoDark;
                    ItemLogo.Source = ItemLogoDark;
                    break;

                case ApplicationTheme.Dark:
                    LogoImage.Source = LogoLight;
                    ItemLogo.Source = ItemLogoLight;
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

        private async void Facebook_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _ = await Launcher.LaunchUriAsync(new Uri(@"https://www.facebook.com/VersatileSoftware"));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            (ViewModel as MainPageViewModel).SelectWebsiteCommand.Execute(null);
        }
    }
}