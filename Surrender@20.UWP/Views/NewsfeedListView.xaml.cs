using MvvmCross.Platforms.Uap.Views;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;
using Windows.UI.Xaml.Controls;

namespace Surrender_20.UWP.View
{
    public sealed partial class NewsfeedListView : MvxWindowsPage
    {
        public NewsfeedListViewModel VM => ViewModel as NewsfeedListViewModel;

        public NewsfeedListView()
        {
            this.InitializeComponent();
        }

        private void GridView_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            VM.ItemSelected.Execute(((GridView)sender).SelectedItem);
        }
    }
}
