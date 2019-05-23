using LeagueOfNews.Forms.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LeagueOfNews.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Master, NoHistory = true, WrapInNavigationPage = false)]
    public partial class MasterView : MvxContentPage<MasterViewModel>
    {
        public MasterView()
        {
            InitializeComponent();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView CollView = (CollectionView)sender;
            ViewModel.NavigateCommand.Execute((MenuListElement)CollView.SelectedItem);
        }
    }
}