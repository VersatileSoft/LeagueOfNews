using HtmlAgilityPack;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.Interface
{

    [AddINotifyPropertyChangedInterface]
    public abstract class INewsfeedItemViewModel : MvxViewModel<Newsfeed>
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
