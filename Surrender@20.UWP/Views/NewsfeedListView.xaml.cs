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
        public NewsfeedListView()
        {
            InitializeComponent();
        }

        private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ((NewsfeedListViewModel)ViewModel).ItemTapped.Execute(((GridView)sender).SelectedItem);
        }
    }
}