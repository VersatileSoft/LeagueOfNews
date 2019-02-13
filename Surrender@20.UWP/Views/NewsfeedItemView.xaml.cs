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

        public NewsfeedItemView()
        {
            InitializeComponent();

            //Workaround: MvxUserControl is custom-made, thus VM is not created by default
             MvxIoCProvider.Instance.RegisterSingleton(MvxIoCProvider.Instance.IoCConstruct<NewsfeedItemViewModel>);

            ViewModel = MvxIoCProvider.Instance.Resolve<NewsfeedItemViewModel>();
            this.DataContext = ViewModel;
        }
    }
}