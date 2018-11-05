using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surrender_20.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab, NoHistory = true, WrapInNavigationPage = false)]
    public partial class NewsfeedListView : MvxContentPage<NewsfeedListViewModel>
    {
		public NewsfeedListView()
		{
			InitializeComponent();
		}

        private void MvxListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {          
            ViewModel.ItemSelected.Execute((Newsfeed)e.SelectedItem);
        }
    }
}