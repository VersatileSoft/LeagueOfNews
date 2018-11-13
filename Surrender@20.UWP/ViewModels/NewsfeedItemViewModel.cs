using HtmlAgilityPack;
using Surrender_20.Core.ViewModels;

namespace Surrender_20.UWP.ViewModels
{ 
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel
    {

        public string Content { get; set; }

        public override void ParseHtml(HtmlNode documentNode)
        {
            Content = documentNode.InnerHtml;
        }
    }
}
