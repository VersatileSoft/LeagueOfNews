using HtmlAgilityPack;
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

        protected HtmlDocument _doc { get; set; }
        public bool IsLoading { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        private readonly IWebClientService _cookieWebClientService;
        private readonly INotificationService _notificationService;

        public NewsfeedItemCoreViewModel(IWebClientService cookieWebClientService, INotificationService notificationService)
        {
            _cookieWebClientService = cookieWebClientService;
            _notificationService = notificationService;
        }

        protected override void InitFromBundle(IMvxBundle parameters)
        {
            Newsfeed s = parameters.Read<Newsfeed>();

            if (!(s.UrlToNewsfeed is null))
            {
                MvxNotifyTask.Create(LoadPage(s));
            }
        }

        public override async void Prepare(Newsfeed newsfeed)
        {
            await LoadPage(newsfeed);
        }

        private async Task LoadPage(Newsfeed newsfeed)
        {
            Title = newsfeed.Title;
            Date = newsfeed.Date;
            IsLoading = true;
            HtmlDocument doc = await _cookieWebClientService.GetPage(newsfeed.UrlToNewsfeed, newsfeed.Page);
            ParseHtml(doc.DocumentNode, newsfeed.Page);
            IsLoading = false;
        }

        public virtual void ParseHtml(HtmlNode documentNode, Pages page)
        {
            switch (page)
            {

                case Pages.Official:
                    // TODO delete not needed nodes from document node


                    //var i = documentNode.SelectSingleNode(".//div[@id='riotbar-bar']");

                    //documentNode.SelectSingleNode("//div[@class='panel-pane pane-lolbar-navigation']").Remove();
                    //    documentNode.SelectSingleNode("//div[@class='panel-pane pane-panels-mini pane-breadcrumb-title']").Remove();

                    //    documentNode.SelectSingleNode("./div[@id='riotbar-subbar']").Remove();
                    break;

                case Pages.SurrenderHome:
                case Pages.ESports:
                case Pages.PBE:
                case Pages.RedPosts:
                case Pages.Rotations:
                case Pages.Releases:
                    // TODO delete not needed nodes from document node
                    break;

            }
        }
    }
}