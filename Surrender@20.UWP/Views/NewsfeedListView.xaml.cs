using System;
using MvvmCross.IoC;
using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using MvvmCross.Views;
using Surrender_20.Core.Interface;
using Surrender_20.UWP.ViewModels;
using Surrender_20.UWP.Views.Custom;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Surrender_20.UWP.View
{
    //Workaround: Generic classes are not supported as a base in UWP XAML
    public abstract class NewsfeedListViewBase : MvxUserControl<NewsfeedListViewModel> { }

    public sealed partial class NewsfeedListView : NewsfeedListViewBase
    {
        public static readonly DependencyProperty PageProperty = DependencyProperty.Register(
            "Page", typeof(Pages), typeof(NewsfeedListView), new PropertyMetadata(Pages.SurrenderHome, new PropertyChangedCallback(OnPageChanged)));

        private static void OnPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        { //FIXME @misiek231 Event does not fire ;(
            var dependency = d as NewsfeedListView;
            dependency.Page = (Pages) e.NewValue;
            dependency.ViewModel.Prepare((Pages) e.NewValue);
        }

        public Pages Page {
            get { return (Pages) GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        public NewsfeedListView()
        {
            InitializeComponent();

            //Workaround: MvxUserControl is custom-made, thus VM is not created by default
            ViewModel = MvxIoCProvider.Instance.IoCConstruct<NewsfeedListViewModel>();
            ViewModel.Prepare(Page); //TODO remove
            this.DataContext = ViewModel;
        }

        private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.ViewModel.ItemTapped.Execute((sender as GridView).SelectedItem);
        }
    }
}