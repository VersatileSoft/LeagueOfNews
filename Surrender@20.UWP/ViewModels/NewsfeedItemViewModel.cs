using HtmlAgilityPack;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Surrender_20.UWP.ViewModels
{
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel, IMvxViewModel<WebView>
    {
        private WebView _webView;
        private IWebClientService _webClientService;

        public NewsfeedItemViewModel(
            IWebClientService webClientService, 
            INotificationService notificationService) 
        : base(webClientService, notificationService) {

            this._webClientService = webClientService;
        }


        public override void ParseHtml(string URL, Pages page)
        {
            _webView.NavigateToString(_webClientService.GetPage(URL, page).ToString());
        }

        public void Prepare(WebView parameter)
        {
            this._webView = parameter;
        }
    }
}