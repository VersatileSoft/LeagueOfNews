﻿using HtmlAgilityPack;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.ComponentModel;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public abstract class NewsfeedItemCoreViewModel : MvxViewModel<Newsfeed>
    {

        protected HtmlDocument _doc { get; set; }
        public bool IsLoading { get; set; }
        public string Title { get; set; }

        public async override void Prepare(Newsfeed newsfeed)
        {
            Title = newsfeed.Title;
            IsLoading = true;
            var doc = await new HtmlWeb().LoadFromWebAsync(newsfeed.UrlToNewsfeed.AbsoluteUri);
            ParseHtml(doc.DocumentNode);
            IsLoading = false;
        }

        public abstract void ParseHtml(HtmlNode documentNode);
    }
}