using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.Threading.Tasks;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class NewsfeedItemCoreViewModel : MvxViewModel<Newsfeed>
    {
        private readonly IWebClientService _cookieWebClientService;
        private readonly ISettingsService _settingsService;

        public bool IsLoading { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }

        public NewsfeedItemCoreViewModel(IWebClientService cookieWebClientService, ISettingsService settingsService)
        {
            _cookieWebClientService = cookieWebClientService;
            _settingsService = settingsService;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            Newsfeed s = parameters.Read<Newsfeed>();

            if (s != null && !string.IsNullOrWhiteSpace(s.UrlToNewsfeed))
            {
                MvxNotifyTask.Create(LoadPage(s));
            }
        }

        public override async void Prepare(Newsfeed newsfeed)
        {
            if (newsfeed != null)
            {
                await LoadPage(newsfeed);
            }
        }

        private Task LoadPage(Newsfeed newsfeed)
        {
            Title = newsfeed.Title;
            Date = newsfeed.Date;
            IsLoading = true;
            //HtmlDocument doc = await _cookieWebClientService.GetPage(newsfeed.UrlToNewsfeed, newsfeed.Page);
            //ParseHtml(doc.DocumentNode, newsfeed.Page);
            ParseHtml(newsfeed.UrlToNewsfeed, _settingsService[newsfeed.Page].Website);
            IsLoading = false;

            return Task.CompletedTask;
        }

        public virtual void ParseHtml(string URL, NewsWebsite page)
        {
            switch (page)
            {
                case NewsWebsite.LoL:
                    // TODO delete not needed nodes from document node


                    //var i = documentNode.SelectSingleNode(".//div[@id='riotbar-bar']");

                    //documentNode.SelectSingleNode("//div[@class='panel-pane pane-lolbar-navigation']").Remove();
                    //    documentNode.SelectSingleNode("//div[@class='panel-pane pane-panels-mini pane-breadcrumb-title']").Remove();

                    //    documentNode.SelectSingleNode("./div[@id='riotbar-subbar']").Remove();
                    break;
                case NewsWebsite.Surrender:
                    // TODO delete not needed nodes from document node
                    break;
            }
        }
    }
}