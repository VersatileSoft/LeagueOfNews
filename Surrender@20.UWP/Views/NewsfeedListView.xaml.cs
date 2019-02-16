using MvvmCross;
using MvvmCross.IoC;
using Surrender_20.UWP.ViewModels;
using Surrender_20.UWP.Views.Custom;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Surrender_20.UWP.View
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
            ViewModel.ItemTapped.Execute((sender as GridView).SelectedItem);
        }
    }
}