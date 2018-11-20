using MvvmCross.Platforms.Uap.Views;
using Surrender_20.UWP.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace Surrender_20.UWP.View
{
    public sealed partial class NewsfeedListView : MvxWindowsPage
    {

        private BitmapImage LogoLight, LogoDark;

        public NewsfeedListView()
        {
            this.InitializeComponent();
            LoadImages();
            ChangeThemeLogo();
        }

        private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((NewsfeedListViewModel)ViewModel).ItemTapped.Execute(((GridView)sender).SelectedItem);
        }

        private void LoadImages()
        {
            LogoLight = new BitmapImage(new Uri("ms-appx:///Assets/Images/NewsfeedAssets/BackgroundLight.png"));
            LogoDark = new BitmapImage(new Uri("ms-appx:///Assets/Images/NewsfeedAssets/BackgroundDark.png"));

        }

        private void MvxWindowsPage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ChangeThemeLogo();
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
