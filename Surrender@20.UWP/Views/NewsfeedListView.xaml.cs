using MvvmCross;
using MvvmCross.IoC;
using LeagueOfNews.UWP.ViewModels;
using LeagueOfNews.UWP.Views.Custom;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace LeagueOfNews.UWP.View
{
    //Workaround: Generic classes are not supported as a base in UWP XAML
    public abstract class NewsfeedListViewBase : MvxUserControl<NewsfeedListViewModel> { }

    public sealed partial class NewsfeedListView : NewsfeedListViewBase
    {
        public NewsfeedListView()
        {
            InitializeComponent();

            //Workaround: MvxUserControl is custom-made, thus VM is not created by default
            ViewModel = MvxIoCProvider.Instance.IoCConstruct<NewsfeedListViewModel>();
            Mvx.IoCProvider.RegisterSingleton(ViewModel);
        }

        private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.ItemSelectedCommand.Execute((sender as GridView).SelectedItem);
        }
    }
}