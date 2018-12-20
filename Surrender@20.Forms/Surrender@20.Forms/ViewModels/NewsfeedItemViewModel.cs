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
                foreach (var item in newsContent.ChildNodes)
                {
                    if (item.Name == "iframe")
                    {
                        stack.Children.Add(new HtmlLabel
                        {
                            Text = cache.ToString()
                        });

                        Debug.Write("[Item] " + item.OuterHtml);
                        Debug.Write("[Cache] " + cache);

                        cache.Clear();

                        stack.Children.Add(new WebView
                        {
                            Source = new HtmlWebViewSource
                            {
                                Html = item.OuterHtml
                            }
                        });
                    }
                    else if (item.Name == "div" && item.HasClass("separator"))
                    {
                        foreach (var subitem in item.Descendants())
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
                                    Source = item.GetAttributeValue("src", "")
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
                                break;
                            } 
                        }
                        cache.AppendLine(item.OuterHtml);
                    }
                    else
                    {
                        cache.AppendLine(item.OuterHtml);
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