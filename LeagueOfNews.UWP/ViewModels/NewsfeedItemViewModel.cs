using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.ViewModels;
using LeagueOfNews.Model;

namespace LeagueOfNews.UWP.ViewModels
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