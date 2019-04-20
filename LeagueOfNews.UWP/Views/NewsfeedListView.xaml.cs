using LeagueOfNews.Model;
using LeagueOfNews.UWP.ViewModels;
using LeagueOfNews.UWP.Views.Custom;
using MvvmCross;
using MvvmCross.IoC;
using System;
using System.IO;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace LeagueOfNews.UWP.View
{
    //Workaround: Generic classes are not supported as a base in UWP XAML
    public abstract class NewsfeedListViewBase : MvxUserControl<NewsfeedListViewModel> { }

    public sealed partial class NewsfeedListView : NewsfeedListViewBase
    {
        public Newsfeed newsfeed = new Newsfeed();

        public NewsfeedListView()
        {
            InitializeComponent();

            //Workaround: MvxUserControl is custom-made, thus VM is not created by default
            ViewModel = MvxIoCProvider.Instance.IoCConstruct<NewsfeedListViewModel>();
            Mvx.IoCProvider.RegisterSingleton(ViewModel);

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;

            if (DataTransferManager.IsSupported())
            {
                //Do sharing
            }
            else
            {
                //SharePost.Visibility = Visibility.Collapsed;
                //CopyLink.Visibility = Visibility.Collapsed;
            }
        }

        private void GridView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.ItemSelectedCommand.Execute((sender as GridView).SelectedItem);
        }

        private async void OpenInBrowser_Click(object sender, RoutedEventArgs e)
        {
            newsfeed = (Newsfeed)(sender as MenuFlyoutItem).DataContext;
            await Launcher.LaunchUriAsync(new Uri(newsfeed.UrlToNewsfeed.Replace("?m=1", "")));
        }

        private void CopyLink_Click(object sender, RoutedEventArgs e)
        {
            newsfeed = (Newsfeed)(sender as MenuFlyoutItem).DataContext;
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(newsfeed.UrlToNewsfeed.Replace("?m=1", ""));
            Clipboard.SetContent(dataPackage);
        }

        private void SharePost_Click(object sender, RoutedEventArgs e)
        {
            newsfeed = (Newsfeed)(sender as MenuFlyoutItem).DataContext;
            DataTransferManager.ShowShareUI();
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            MemoryStream stream = new MemoryStream(newsfeed.Image);

            request.Data.SetText(newsfeed.ShortDescription);
            request.Data.SetWebLink(new Uri(newsfeed.UrlToNewsfeed));
            request.Data.SetBitmap(RandomAccessStreamReference.CreateFromStream(stream.AsRandomAccessStream()));

            request.Data.Properties.Title = newsfeed.Title;
            request.Data.Properties.Description = newsfeed.ShortDescription;
            request.Data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromStream(stream.AsRandomAccessStream());
            request.Data.Properties.ContentSourceWebLink = new Uri(newsfeed.UrlToNewsfeed);
        }
    }
}