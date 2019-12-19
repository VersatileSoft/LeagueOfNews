using System;
using LeagueOfNews.Forms.ViewModels;
using LeagueOfNews.Model;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace LeagueOfNews.Forms.Views.CategoryViews
{
    [MvxTabbedPagePresentation(TabbedPosition.Tab, NoHistory = true)]
    public partial class NewsfeedCategoryListView : MvxContentPage<NewsfeedCategoryListViewModel>
    {
        public NewsfeedCategoryListView() => InitializeComponent();

        private void InfiniteCollectionView_ItemTapped(object sender, EventArgs e)
        {
            //Execute command
            CollectionView CollView = (CollectionView)sender;
            ViewModel.ItemSelectedCommand.Execute((Newsfeed)CollView.SelectedItem);

            //Reset selection
            CollView.SelectedItem = null;
        }
    }
}