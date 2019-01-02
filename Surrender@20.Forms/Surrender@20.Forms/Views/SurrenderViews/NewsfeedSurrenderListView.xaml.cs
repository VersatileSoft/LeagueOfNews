using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Forms.ViewModels;
using Surrender_20.Forms.ViewModels.SurrenderViewModels;
using Surrender_20.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surrender_20.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxTabbedPagePresentation(TabbedPosition.Tab, NoHistory = true)]
    public partial class NewsfeedSurrenderListView : MvxContentPage<NewsfeedSurrenderListViewModel>
    {
        public NewsfeedSurrenderListView()
        {
            InitializeComponent();
        }

        private void MvxListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.ItemTapped.Execute((Newsfeed)e.Item);
        }
    }
}