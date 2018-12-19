using HtmlAgilityPack;
using LabelHtml.Forms.Plugin.Abstractions;
using PropertyChanged;
using Surrender_20.Core.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Surrender_20.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel
    {
        public View Content { get; set; }

        public override void ParseHtml(HtmlNode documentNode)
        {

            var newsContent =
                documentNode.SelectSingleNode("//*[contains(@class,'news-content')]");

            var tableOfContents =
                newsContent.SelectNodes("div[@id='toc']|div[following-sibling::div[@id='toc']][1]");

            if (tableOfContents != null)
            {
                foreach (var item in tableOfContents)
                {
                    item.Remove();
                }
            }

            //TODO add youtube
            //TODO add gallery view/higher resolution image redirect

            Content = new HtmlLabel
            {
                Text = newsContent.InnerHtml
            };
        }
    }
}
