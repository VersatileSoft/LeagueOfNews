using MvvmCross.Platforms.Uap.Views;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Surrender_20.UWP.View
{
    public sealed partial class NewsfeedListView : MvxWindowsPage
    {
        public NewsfeedListViewModel VM => ViewModel as NewsfeedListViewModel;

        public NewsfeedListView()
        {
            this.InitializeComponent();
        }

        private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            VM.ItemSelected.Execute(((GridView)sender).SelectedItem);
        }

        //zlewam pull-to-refresh bo psuje design
        /*private void RefreshContainer_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args)
        {
            RefreshContainer.RequestRefresh(); //idk czy to działa, musze tablet zaktualizować xD
        }*/
    }
}
