using HtmlAgilityPack;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class NewsfeedItemCoreViewModel : MvxViewModel<Newsfeed>
    {

        protected HtmlDocument _doc { get; set; }
        public bool IsLoading { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        private ICookieWebClientService _cookieWebClientService;

        public NewsfeedItemCoreViewModel(ICookieWebClientService cookieWebClientService)
        {
            _cookieWebClientService = cookieWebClientService;
        }

        public async override void Prepare(Newsfeed newsfeed)
        {
            Title = newsfeed.Title;
            Date = newsfeed.Date;
            IsLoading = true;
            var doc = await _cookieWebClientService.GetPage(newsfeed.UrlToNewsfeed.AbsoluteUri);
            ParseHtml(doc.DocumentNode, newsfeed.Page);
            IsLoading = false;
        }

        public abstract void ParseHtml(HtmlNode documentNode, Pages page);
    }
}