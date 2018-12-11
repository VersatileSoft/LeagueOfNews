using HtmlAgilityPack;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.ComponentModel;

namespace Surrender_20.Core.ViewModels
{
    
    public abstract class NewsfeedItemCoreViewModel : MvxViewModel<Newsfeed>, INotifyPropertyChanged
    {

        protected HtmlDocument _doc { get; set; }
        public bool IsLoading { get; set; }

        public async override void Prepare(Newsfeed newsfeed)
        {
            IsLoading = true;
            var doc = await new HtmlWeb().LoadFromWebAsync(newsfeed.UrlToNewsfeed.AbsoluteUri);
            ParseHtml(doc.DocumentNode);
            IsLoading = false;
        }

        public abstract void ParseHtml(HtmlNode documentNode);
    }
}
