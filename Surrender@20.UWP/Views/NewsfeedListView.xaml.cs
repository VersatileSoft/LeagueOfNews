using System;
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
    public sealed partial class NewsfeedListView : MvxUserControl<NewsfeedListViewModel>
    {
        public static readonly DependencyProperty PageProperty = DependencyProperty.Register(
            "Page", typeof(Pages), typeof(NewsfeedListView), new PropertyMetadata(Pages.SurrenderHome, new PropertyChangedCallback(OnPageChanged)));

        private static void OnPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((Pages) e.NewValue != (Pages) e.OldValue)
            {
                (d as MvxUserControl<NewsfeedListViewModel>).ViewModel.Prepare((Pages) e.NewValue);
            }
        }

        /*
        public Pages Page {
            get { return (Page) base.GetValue(PageProperty); }
            set { base.SetValue(PageProperty, value); }
        }
        */

        public NewsfeedListView()
        {
            InitializeComponent();
        }

        private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.ViewModel.ItemTapped.Execute((sender as GridView).SelectedItem);
        }
    }
}