using HtmlAgilityPack;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedItemRootViewModel : MvxViewModel<Newsfeed>
    {
        protected HtmlDocument _doc { get; set; }
        public bool IsLoading { get; set; }

        public async override void Prepare(Newsfeed newsfeed)
        {
            IsLoading = true;
            var doc = await new HtmlWeb().LoadFromWebAsync(newsfeed.UrlToNewsfeed.AbsoluteUri);
            _doc = doc;
        }
    }
}
