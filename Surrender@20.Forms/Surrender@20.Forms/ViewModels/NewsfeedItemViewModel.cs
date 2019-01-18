using HtmlAgilityPack;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;

namespace Surrender_20.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedItemViewModel : NewsfeedItemCoreViewModel
    {

        private string _content;

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public NewsfeedItemViewModel(IWebClientService cookieWebClientService, INotificationService notificationService) : base(cookieWebClientService, notificationService)
        {

        }

        public override void ParseHtml(HtmlNode documentNode, Pages page)
        {
            base.ParseHtml(documentNode, page);
            Content = documentNode.InnerHtml;
        }
    }
}

/*
 * 
 * private readonly IMvxNavigationService _navigationService;

        private View _content;
        private ImageSource _thumbnailSource;

        public View Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        public ImageSource ThumbnailSource
        {
            get => _thumbnailSource;
            set => SetProperty(ref _thumbnailSource, value);
        }

        public NewsfeedItemViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void ParseHtml(HtmlNode documentNode)
        {
            StackLayout stack = new StackLayout();

            HtmlNode newsContent = documentNode
                .SelectSingleNode("//div[contains(@class,'news-content')]");

            HtmlNodeCollection tableOfContents = newsContent
                .SelectNodes("./div[@id='toc']|div[following-sibling::h2[@id='toc']][1]");

            ThumbnailSource = newsContent
                .SelectSingleNode("./div[contains(@class,'separator')][1]/a/img")
                .GetAttributeValue("src", "");

            if (tableOfContents != null)
            {
                foreach (HtmlNode item in tableOfContents)
                {
                    item.Remove();
                }
            }

            if (newsContent != null)
            {
                StringBuilder cache = new StringBuilder();
                HtmlNodeCollection arr = newsContent.ChildNodes;
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
                        foreach (HtmlNode subitem in arr[i].Descendants(5))
                        {
                            if (subitem.Name == "img")
                            {
                                stack.Children.Add(new HtmlLabel
                                {
                                    Text = cache.ToString()
                                });

                                cache.Clear();

                                Image Image = new Image
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
 * 
 */
