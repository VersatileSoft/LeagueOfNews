using HtmlAgilityPack;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;
using Windows.UI.Xaml.Controls;

namespace Surrender_20.UWP.ViewModels
{
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel, IMvxViewModel<WebView>
    {
        private WebView _webView;
        private IWebClientService _webClientService;

        public Newsfeed Newsfeed { get; set; }

        public NewsfeedItemViewModel(
            IWebClientService webClientService, 
            INotificationService notificationService) 
                : base(webClientService, notificationService)
        {
            this._webClientService = webClientService;
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            if (Newsfeed != null)
            {
                ParseHtml(Newsfeed.UrlToNewsfeed, Newsfeed.Page);
            }
        }

        public override void ParseHtml(string URL, Pages page)
        {
            _webView.NavigateToString(_webClientService.GetPage(URL, page).ToString()); //FIXME webView is null where it shouldn't be :c
        }

        public void Prepare(WebView parameter)
        {
            this._webView = parameter;
        }
    }
}