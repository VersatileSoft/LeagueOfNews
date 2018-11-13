using HtmlAgilityPack;

namespace Surrender_20.Forms.ViewModels
{ 
    public class NewsfeedItemViewModel : Core.ViewModels.NewsfeedItemViewModel
    {

        public string Content { get; set; }

        public override void ParseHtml(HtmlNode documentNode)
        {
            Content = documentNode.InnerHtml;
        }
    }
}
