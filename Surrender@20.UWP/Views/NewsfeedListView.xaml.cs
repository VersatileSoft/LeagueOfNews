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
        public NewsfeedListViewModel VM => ViewModel as NewsfeedListViewModel;

        private BitmapImage LogoLight, LogoDark;

        public NewsfeedListView()
        {
            this.InitializeComponent();
            LoadImages();
            ChangeThemeLogo();
        }

        private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            VM.ItemTapped.Execute(((GridView)sender).SelectedItem);
        }

        //zlewam pull-to-refresh bo psuje design
        /*private void RefreshContainer_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {
            RefreshContainer.RequestRefresh(); //idk czy to działa, musze tablet zaktualizować xD
        }*/

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
