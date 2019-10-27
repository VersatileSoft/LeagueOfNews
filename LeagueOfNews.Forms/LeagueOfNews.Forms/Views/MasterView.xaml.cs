using LeagueOfNews.Forms.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace LeagueOfNews.Forms.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master, NoHistory = true, WrapInNavigationPage = false)]
    public partial class MasterView : MvxContentPage<MasterViewModel>
    {
        public MasterView()
        {
            InitializeComponent();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Execute command
            CollectionView CollView = (CollectionView)sender;
            ViewModel.NavigateCommand.Execute((MenuListElement)CollView.SelectedItem);
        }
    }
}