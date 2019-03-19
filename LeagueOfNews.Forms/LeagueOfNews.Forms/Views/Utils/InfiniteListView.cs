using MvvmCross.Forms.Views;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace LeagueOfNews.Forms.Views.Utils
{
    public class InfiniteListView : MvxListView
    {
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create(nameof(LoadMoreCommand), typeof(ICommand), typeof(InfiniteListView), default(ICommand));
        public ICommand LoadMoreCommand
        {
            get => (ICommand)GetValue(LoadMoreCommandProperty);
            set => SetValue(LoadMoreCommandProperty, value);
        }

        public InfiniteListView()
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
        }

        private void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            if (ItemsSource is IList items && e.Item == items[items.Count - 2])
            {
                if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                {
                    LoadMoreCommand.Execute(null);
                }
            }
        }
    }
}