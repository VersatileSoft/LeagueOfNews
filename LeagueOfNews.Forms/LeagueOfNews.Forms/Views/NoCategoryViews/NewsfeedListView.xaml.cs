using LeagueOfNews.Forms.ViewModels;
using LeagueOfNews.Model;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using Xamarin.Forms;

namespace LeagueOfNews.Forms.Views.NoCategoryViews
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true)]
    public partial class NewsfeedListView : MvxContentPage<NewsfeedListViewModel>
    {
        public NewsfeedListView()
        {
            InitializeComponent();
        }

        private void InfiniteCollectionView_ItemTapped(object sender, EventArgs e)
        {
            //Execute command
            CollectionView CollView = (CollectionView)sender;
            ViewModel.ItemSelectedCommand.Execute((Newsfeed)CollView.SelectedItem);

            //Reset selection
            CollView.SelectedItem = null;
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    ViewModel.LoadMoreCommand.Execute(sender);
        //}
    }
}