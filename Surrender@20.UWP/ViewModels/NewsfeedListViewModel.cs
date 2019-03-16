using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.ViewModels;
using LeagueOfNews.Model;
using System.Threading.Tasks;

namespace LeagueOfNews.UWP.ViewModels
{
    public class NewsfeedListViewModel : NewsfeedListCoreViewModel, IMvxViewModel<NewsCategory>
    {
        public NewsfeedListViewModel(INewsfeedService newsfeedService, ISettingsService settingsService, IMvxNavigationService navigationService)
           : base(newsfeedService, settingsService, navigationService)
        {
        }

        public void Prepare(NewsCategory parameter)
        {
            Title = _settingsService[parameter].Title;
            SelectedCategory = parameter;
            LoadNewsfeeds();
        }

        protected override Task NavigateToAsync(Newsfeed newsfeed)
        {
            var itemVM = MvxIoCProvider.Instance.Resolve<NewsfeedItemViewModel>();
            itemVM.Prepare(newsfeed);

            return Task.CompletedTask;
        }
    }
}