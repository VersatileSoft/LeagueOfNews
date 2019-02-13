﻿using MvvmCross.Base;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using Surrender_20.UWP.Views.MessageBoxes;
using System;
using System.Net.NetworkInformation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Surrender_20.UWP.View
{
    public sealed partial class MainPageView : MvxWindowsPage
    {
        private BitmapImage LogoLight, LogoDark;
        private readonly ConnectionDialog ConnectionDialog = new ConnectionDialog();
        private readonly SiteChooseDialog SiteChooseDialog = new SiteChooseDialog();

        private IMvxInteraction<Func<bool>> _checkInternetConnectionInteraction;
        public IMvxInteraction<Func<bool>> CheckInternetConnectionInteraction {
            get => _checkInternetConnectionInteraction;
            set {
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

        private async void ChangeSite_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await SiteChooseDialog.ShowAsync();
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

        /*private void NavView_ItemSelected(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;

            if (item.Content.Equals("Home"))
            {
                ContentFrame.Navigate(typeof(NewsfeedListView), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Statystyki postaci";
            }
            else if (item.Content.Equals("PBE"))
            {
                ContentFrame.Navigate(typeof(NewsfeedListViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Siła czarów i run";
            }
            else if (item.Content.Equals("Releases"))
            {
                ContentFrame.Navigate(typeof(NewsfeedListViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Umiejętności";
            }
            else if (item.Content.Equals("Red Posts"))
            {
                ContentFrame.Navigate(typeof(NewsfeedListViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Lista przedmiotów";
            }
            else if (item.Content.Equals("Rotations"))
            {
                ContentFrame.Navigate(typeof(NewsfeedListViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Poradniki Youtube";
            }
            else if (item.Content.Equals("E-Sports"))
            {
                ContentFrame.Navigate(typeof(NewsfeedListViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Poradniki Youtube";
            }
            else if (item.Content.Equals("Settings"))
            {
                ContentFrame.Navigate(typeof(SettingsViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Informacje";
            }
        }*/
    }
}