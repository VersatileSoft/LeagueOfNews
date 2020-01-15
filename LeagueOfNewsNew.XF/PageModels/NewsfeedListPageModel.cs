using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace LeagueOfNewsNew.XF.PageModels
{
    public class NewsfeedListPageModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string title = "Siema";
        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                {
                    title = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
                }
            }
        }

        public NewsfeedListPageModel()
        {
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                Title = "Elo";
                return true;
            });
        }
    }
}
