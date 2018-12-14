using HtmlAgilityPack;
using Surrender_20.Core.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Surrender_20.Forms.ViewModels
{
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel, INotifyPropertyChanged
    {
        //private string _content;
        //public string Content
        //{
        //    get
        //    {
        //        return _content;
        //    }
        //    set
        //    {
        //        SetProperty(ref _content, value);
        //    }
        //}

        private View _content;
        public View Content
        {
            get
            {
                return _content;
            }
            set
            {
                SetProperty(ref _content, value);
            }
        }

        //public string Content { get; set; }

        public override void ParseHtml(HtmlNode documentNode)
        {
            StackLayout stack = new StackLayout();

            //Content = documentNode.InnerHtml; WYJEBAŁEM BO SIE NIE KOMPILOWAŁO <3 ~Kapi
        }
    }
}
