using HtmlAgilityPack;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class NewsfeedItemViewModel 
    {

        async override void Prepare(Newsfeed newsfeed)
        {
            IsLoading = true;
            var doc = await new HtmlWeb().LoadFromWebAsync(newsfeed.UrlToNewsfeed.AbsoluteUri);
            ParseHtml(doc.DocumentNode);
            IsLoading = false;
        }
    }
}
