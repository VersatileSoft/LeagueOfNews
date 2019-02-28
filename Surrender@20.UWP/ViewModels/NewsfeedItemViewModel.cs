using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;

namespace Surrender_20.UWP.ViewModels
{
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel
    {
        private string _url;

        public Newsfeed Newsfeed { get; set; }

        public string URL
        {
            get => _url;
            set => SetProperty(ref _url, value);
        }

        public NewsfeedItemViewModel(IWebClientService webClientService, ISettingsService settingsService)
            : base(webClientService, settingsService)
        {
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            if (Newsfeed != null)
            {
                ParseHtml(Newsfeed.UrlToNewsfeed, Newsfeed.Website);
            }
        }
        
        public override void ParseHtml(string _url, NewsWebsite page)
        {
            base.ParseHtml(URL, page);
            URL = _url;
        }
    }
}