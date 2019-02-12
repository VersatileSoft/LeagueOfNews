using MvvmCross.Platforms.Uap.Views;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Surrender_20.UWP.Views.Custom
{
    public class MvxUserControl : UserControl, IMvxWindowsView, IDisposable
    {
        public IMvxViewModel _viewModel;
        public IMvxViewModel ViewModel {
            get {
                return _viewModel;
            }
            set {
                if (_viewModel == value)
                    return;

                _viewModel = value;
                DataContext = ViewModel;
                OnViewModelSet();
            }
        }

        protected virtual void OnViewModelSet() {}

        public MvxUserControl()
        {
            Loading += MvxUserControl_Loading;
            Loaded += MvxUserControl_Loaded;
            Unloaded += MvxUserControl_Unloaded;
        }

        private void MvxUserControl_Loading(FrameworkElement sender, object args)
        {
            ViewModel?.ViewAppearing();
        }

        private void MvxUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewAppeared();
        }

        private void MvxUserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewDestroy();
        }

        public void ClearBackStack()
        {
            //TODO remove content?
        }

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MvxUserControl()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Loading -= MvxUserControl_Loading;
                Loaded -= MvxUserControl_Loaded;
                Unloaded -= MvxUserControl_Unloaded;
            }
        }
        #endregion
    }

    public class MvxUserControl<TViewModel> : MvxUserControl, IMvxWindowsView<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel {
            get { return (TViewModel) base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
