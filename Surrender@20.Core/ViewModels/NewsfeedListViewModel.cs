using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Surrender_20.Core.ViewModels
{
    public class NewsfeedListViewModel : MvxViewModel<NewsfeedNavigationParameter>
    {
        private string _url;
        private INewsfeedService _newsfeedService;

        public List<Newsfeed> Newsfeeds { get; set; }
        public string Title { get; set; }
        public bool IsLoading { get; set; }

        public NewsfeedListViewModel(IMvxNavigationService navigationService, INewsfeedService newsfeedService)
        {
            _newsfeedService = newsfeedService;
        }

        public override void Prepare(NewsfeedNavigationParameter parameter)
        {
            Title = parameter.Title;
            _url = parameter.URL;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            IsLoading = true;
            Newsfeeds = _newsfeedService.LoadNewsfeeds(_url);
            IsLoading = false;
        }
    }
}
