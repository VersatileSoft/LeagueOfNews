using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;
using System.Collections.Generic;
using System.Linq;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedListViewModel : BaseViewModel
    {
        [DoNotSetChanged] //TODO check if private access does not do that by default
        private string _url { get; set; }
        public string Title { get; set; }
        private INewsfeedService _newsfeedService;

        public List<Newsfeed> Newsfeeds { get; set; }
        public bool IsLoading { get; set; } = false;

        public NewsfeedListViewModel(IMvxNavigationService navigationService, INewsfeedService newsfeedService) :
           base(navigationService)
        {
            _newsfeedService = newsfeedService;
        }

        public override void Prepare(MvxBundle parameter)
        {
            Title = parameter.Data.Keys.ToList()[0];
            _url = parameter.Data.Values.ToList()[0];

            Newsfeeds =_newsfeedService.LoadNewsfeeds(_url);
        }
    }
}
