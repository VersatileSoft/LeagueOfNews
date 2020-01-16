using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LeagueOfNews.Model;
using LeagueOfNewsNew.XF.Services.Interfaces;
using PropertyChanged;

namespace LeagueOfNewsNew.XF.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedListPageModel : PageModelBase<int>
    {
        private readonly INewsfeedService _newsfeedService;
        public ObservableCollection<Newsfeed> Newsfeeds { get; set; }
        public bool IsLoading { get; set; }
        public NewsfeedListPageModel(INewsfeedService newsfeedService) => _newsfeedService = newsfeedService;

        public override async Task OnLoad()
        {
            IsLoading = true;
            Newsfeeds = new ObservableCollection<Newsfeed>(await _newsfeedService.GetNewsfeeds(Param));
            IsLoading = false;
        }
    }
}
