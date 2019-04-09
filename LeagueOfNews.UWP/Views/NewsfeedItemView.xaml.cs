using LeagueOfNews.UWP.ViewModels;
using LeagueOfNews.UWP.Views.Custom;
using MvvmCross;
using MvvmCross.IoC;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace LeagueOfNews.UWP.View
{
    //Workaround: Generic classes are not supported as a base in UWP XAML
    public abstract class NewsfeedItemViewBase : MvxUserControl<NewsfeedItemViewModel> { }

    public sealed partial class NewsfeedItemView : NewsfeedItemViewBase
    {
        public NewsfeedItemView()
        {
            InitializeComponent();

            //Workaround: MvxUserControl is custom-made, thus VM is not created by default
            ViewModel = MvxIoCProvider.Instance.IoCConstruct<NewsfeedItemViewModel>();
            Mvx.IoCProvider.RegisterSingleton(ViewModel);

            NewsfeedWebView.NavigationCompleted += Webview_navigationCompleted;
            NewsfeedWebView.NavigationStarting += Webview_navigationStarting;
        }

        private void Webview_navigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            LoadingControl.IsLoading = false;
        }

        private void Webview_navigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            LoadingControl.IsLoading = true;
        }

        private void NewsfeedWebView_ContainsFullScreenElementChanged(WebView sender, object args)
        {
            ApplicationView applicationView = ApplicationView.GetForCurrentView();

            if (sender.ContainsFullScreenElement)
            {
                applicationView.TryEnterFullScreenMode();
            }
            else if (applicationView.IsFullScreenMode)
            {
                applicationView.ExitFullScreenMode();
            }
        }
    }
}