using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using MvvmCross.Platforms.Uap.Views;
using Surrender_20.Core.ViewModels;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Surrender_20
{
    public sealed partial class MainPageView : MvxWindowsPage
    {
        public MainPageViewModel VM => ViewModel as MainPageViewModel;

        public MainPageView()
        {
            this.InitializeComponent();

            var CoreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            CoreTitleBar.ExtendViewIntoTitleBar = true;
            Window.Current.SetTitleBar(DragArea);
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        private void NavView_ItemSelected(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            NavigationViewItem item = args.SelectedItem as NavigationViewItem;

            if (item.Name == "LevelPage")
            {
                ContentFrame.Navigate(typeof(NewsfeedListViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Statystyki postaci";
            }
            else if (item.Name == "MagicPage")
            {
                ContentFrame.Navigate(typeof(NewsfeedListViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Siła czarów i run";
            }
            else if (item.Name == "SkillsPage")
            {
                ContentFrame.Navigate(typeof(NewsfeedListViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Umiejętności";
            }
            else if (item.Name == "ItemsListPage")
            {
                ContentFrame.Navigate(typeof(NewsfeedListViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Lista przedmiotów";
            }
            else if (item.Name == "TutorialsPage")
            {
                ContentFrame.Navigate(typeof(NewsfeedListViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Poradniki Youtube";
            }
            else if (item.Content.Equals("Settings"))
            {
                ContentFrame.Navigate(typeof(SettingsViewModel), null, new DrillInNavigationTransitionInfo());
                //TitlePageTextBlock.Text = "Informacje";
            }
        }
    }
}