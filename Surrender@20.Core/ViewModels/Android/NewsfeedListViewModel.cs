using MvvmCross.Commands;
using MvvmCross.ViewModels;
using Surrender_20.Core.Model;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels.Android
{
    public class NewsfeedListViewModel : MvxViewModel
    {
        private TabListItem _selectedItem;

        public NewsfeedListViewModel()
        {
            Items = new ObservableCollection<TabListItem> {
                new TabListItem { Title = "title one" },
                new TabListItem { Title = "title two" },
                new TabListItem { Title = "title three" },
                new TabListItem { Title = "title four" },
                new TabListItem { Title = "title five" },
            };
        }

        private ObservableCollection<TabListItem> _items;

        public ObservableCollection<TabListItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public TabListItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);
            }
        }

        public virtual ICommand ItemSelected
        {
            get
            {
                return new MvxCommand<TabListItem>(item => {
                    SelectedItem = item;
                });
            }
        }

        private bool _isRefreshing;

        public virtual bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        public ICommand ReloadCommand
        {
            get
            {
                return new MvxCommand(async () =>
                {
                    IsRefreshing = true;

                    await ReloadData();

                    IsRefreshing = false;
                });
            }
        }

        public virtual async Task ReloadData()
        {
            // By default return a completed Task
            await Task.Delay(5000);

            var rand = new Random();
            Func<char> randChar = () => (char)rand.Next(65, 90);
            Func<int, string> randStr = null;
            randStr = x => (x > 0) ? randStr(--x) + randChar() : "";

            var newItemCount = rand.Next(3);

            for (var i = 0; i < newItemCount; i++)
                Items.Add(new TabListItem { Title = "title " + randStr(4) });
        }
    }
}