using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;

namespace Surrender_20.UWP.ViewModels
{
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel
    {
        public string Content { get; set; }

        public NewsfeedItemViewModel(IWebClientService cookieWebClientService, INotificationService notificationService) : base(cookieWebClientService, notificationService) { }

        public override void ParseHtml(HtmlNode documentNode, Pages page)
        {
            base.ParseHtml(documentNode, page);
            Content = documentNode.InnerHtml;
        }
    }
}