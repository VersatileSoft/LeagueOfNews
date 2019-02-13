using System;
using MvvmCross.IoC;
using MvvmCross.Platforms.Uap.Views;
using Surrender_20.Model;
using Surrender_20.UWP.ViewModels;
using Surrender_20.UWP.Views.Custom;
using Windows.UI.Xaml;

namespace Surrender_20.UWP.View
{
    //Workaround: Generic classes are not supported as a base in UWP XAML
    public abstract class NewsfeedItemViewBase : MvxUserControl<NewsfeedItemViewModel> { }

    public sealed partial class NewsfeedItemView : NewsfeedItemViewBase
    {
        public static readonly DependencyProperty ItemProperty = DependencyProperty.Register(
            "Item", typeof(Newsfeed), typeof(NewsfeedItemView), new PropertyMetadata(null, OnItemChanged));

        private static void OnItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((Newsfeed) e.OldValue != (Newsfeed) e.NewValue)
            {
                var dependency = d as NewsfeedItemView;
                dependency.Item = (Newsfeed) e.NewValue;
                dependency.ViewModel.Newsfeed = (Newsfeed) e.NewValue;
            }
        }

        public Newsfeed Item {
            get { return (Newsfeed) GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        public NewsfeedItemView()
        {
            InitializeComponent();

            //Workaround: MvxUserControl is custom-made, thus VM is not created by default
            ViewModel = MvxIoCProvider.Instance.IoCConstruct<NewsfeedItemViewModel>();
            ViewModel.Prepare(NewsfeedWebView);
            this.DataContext = ViewModel;
        }
    }
}