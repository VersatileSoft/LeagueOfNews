using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Model;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedItemViewModel : MvxViewModel<Newsfeed>
    {
        public Newsfeed Newsfeed { get; set; }
        public string Content { get; set; }
        public bool IsLoading { get; set; }
        private INewsfeedPageService _newsfeedPageParser;

        public NewsfeedItemViewModel(INewsfeedPageService newsfeedPageParser)
        {
            _newsfeedPageParser = newsfeedPageParser;
        }

        public async override void Prepare(Newsfeed newsfeed)
        {
            Newsfeed = newsfeed;
            IsLoading = true;
            Content = await _newsfeedPageParser.ParseNewsfeed(Newsfeed.UrlToNewsfeed);
            IsLoading = false;
        }
    }
}
