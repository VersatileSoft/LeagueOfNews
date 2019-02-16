using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;

namespace Surrender_20.UWP.ViewModels
{
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel
    {
        public Newsfeed Newsfeed { get; set; }

        private string _url;

        public string URL //Fody takes care of it... Or is it?
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public NewsfeedItemViewModel(
            IWebClientService webClientService,
            INotificationService notificationService)
                : base(webClientService, notificationService)
        {

        }

        //public override void ViewAppearing()
        //{
        //    base.ViewAppearing();

        //    if (Newsfeed != null)
        //    {
        //        ParseHtml(Newsfeed.UrlToNewsfeed, Newsfeed.Page);
        //    }
        //}

        public override void ParseHtml(string _url, NewsCategory page)
        {
            base.ParseHtml(URL, page);
            URL = _url;
        }
    }
}