using HtmlAgilityPack;
using LabelHtml.Forms.Plugin.Abstractions;
using MvvmCross.Commands;
using MvvmCross.Forms.Presenters;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace Surrender_20.Forms.ViewModels
{
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel
    {
        private IMvxNavigationService _navigationService;

        private View _content;
        private ImageSource _thumbnailSource;

        public View Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        public ImageSource ThumbnailSource {
            get { return _thumbnailSource; }
            set { SetProperty(ref _thumbnailSource, value); }
        }

        public NewsfeedItemViewModel(IMvxNavigationService navigationService)
        {
            this._navigationService = navigationService;
        }

        public override void ParseHtml(HtmlNode documentNode)
        {
            StackLayout stack = new StackLayout();

            var newsContent = documentNode
                .SelectSingleNode("//div[contains(@class,'news-content')]");

            var tableOfContents = newsContent
                .SelectNodes("./div[@id='toc']|div[following-sibling::h2[@id='toc']][1]");

            ThumbnailSource = newsContent
                .SelectSingleNode("./div[contains(@class,'separator')][1]/a/img")
                .GetAttributeValue("src", "");

            if (tableOfContents != null)
            {
                foreach (var item in tableOfContents)
                {
                    item.Remove();
                }
            }

            if (newsContent != null)
            {
                var cache = new StringBuilder();
                var arr = newsContent.ChildNodes;
                for (int i = 0; i < arr.Count; i++)
                {
                    if (arr[i].Name == "iframe")
                    {
                        stack.Children.Add(new HtmlLabel
                        {
                            Text = cache.ToString()
                        });

                        cache.Clear();

                        stack.Children.Add(new WebView
                        {
                            Source = new HtmlWebViewSource
                            {
                                Html = arr[i].OuterHtml
                            }
                        });
                    }
                    else if (arr[i].Name == "div" && (arr[i].HasClass("separator") || i == newsContent.ChildNodes.Count - 5)) //TODO check
                    {
                        Debug.Write("[Item] " + arr[i].OuterHtml);
                        Debug.Write("[Cache] " + cache);

                        bool foundImg = false;
                        foreach (var subitem in arr[i].Descendants(5))
                        {
                            if (subitem.Name == "img")
                            {
                                stack.Children.Add(new HtmlLabel
                                {
                                    Text = cache.ToString()
                                });

                                cache.Clear();

                                var Image = new Image
                                {
                                    Source = subitem.GetAttributeValue("src", "")
                                };

                                Image.GestureRecognizers.Add(new TapGestureRecognizer
                                {
                                    Command = new MvxCommand(() =>
                                    {
                                        //TODO navigate to custom MvxCarouselPage
                                        //_navigationService.Navigate()
                                    })
                                });

                                stack.Children.Add(Image);
                                foundImg = true;
                                break;
                            }
                        }

                        if (!foundImg)
                        {
                            cache.AppendLine(arr[i].OuterHtml);
                        }
                    }
                    else
                    {
                        cache.AppendLine(arr[i].OuterHtml);
                    }
                }

                if (cache.Length != 0)
                {
                    stack.Children.Add(new HtmlLabel
                    {
                        Text = cache.ToString()
                    });
                }
            }

            //TODO add youtube

            Content = stack;
        }
    }
}