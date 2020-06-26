using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.Services.Interfaces;
using MvvmHelpers.Commands;
using PropertyChanged;
using Xamarin.Essentials;

namespace LeagueOfNewsNew.XF.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedListPageModel : PageModelBase<int>
    {
        private readonly INewsfeedService _newsfeedService;
        public ObservableCollection<Newsfeed> Newsfeeds { get; set; }
        public bool IsLoading { get; set; }
        public ICommand ItemSelectedCommand { get; set; }

        public NewsfeedListPageModel(INewsfeedService newsfeedService)
        {
            _newsfeedService = newsfeedService;
            ItemSelectedCommand = new AsyncCommand<Newsfeed>(ItemSelected);
        }

        public override async Task OnLoad()
        {
            IsLoading = true;
            Newsfeeds = new ObservableCollection<Newsfeed>(await _newsfeedService.GetNewsfeeds(Param));
            IsLoading = false;
        }

        private Task ItemSelected(Newsfeed newsfeed) => Browser.OpenAsync(newsfeed.UrlToNewsfeed, new BrowserLaunchOptions
        {
            LaunchMode = BrowserLaunchMode.SystemPreferred,
            TitleMode = BrowserTitleMode.Show,
        });
    }
}
