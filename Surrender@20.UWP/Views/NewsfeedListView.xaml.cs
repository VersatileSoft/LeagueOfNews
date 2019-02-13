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
        //public static readonly DependencyProperty PageProperty = DependencyProperty.Register(
        //    "Page", typeof(Pages), typeof(NewsfeedListView), new PropertyMetadata(Pages.SurrenderHome, new PropertyChangedCallback(OnPageChanged)));

        //private static void OnPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{ //FIXME @misiek231 Event does not fire ;(
        //    var dependency = d as NewsfeedListView;
        //    dependency.Page = (Pages) e.NewValue;
        //    dependency.ViewModel.Prepare((Pages) e.NewValue);
        //}

        //public Pages Page {
        //    get { return (Pages) GetValue(PageProperty); }
        //    set { SetValue(PageProperty, value); }
        //}

        public NewsfeedListView()
        {
            InitializeComponent();

            //Workaround: MvxUserControl is custom-made, thus VM is not created by default
            Mvx.IoCProvider.RegisterSingleton(MvxIoCProvider.Instance.IoCConstruct<NewsfeedListViewModel>());
            ViewModel = MvxIoCProvider.Instance.Resolve<NewsfeedListViewModel>();
            DataContext = ViewModel;
        }

        private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.ItemTapped.Execute((sender as GridView).SelectedItem);
        }
    }
}