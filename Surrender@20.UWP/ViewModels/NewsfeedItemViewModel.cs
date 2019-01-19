using HtmlAgilityPack;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;

namespace Surrender_20.UWP.ViewModels
{
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel
    {
        public string Content { get; set; }

        public NewsfeedItemViewModel(ICookieWebClientService cookieWebClientService) : base(cookieWebClientService) { }

        public override void ParseHtml(HtmlNode documentNode, Pages page)
        {
            base.ParseHtml(documentNode, page);
            Content = documentNode.InnerHtml;
        }
    }
}
