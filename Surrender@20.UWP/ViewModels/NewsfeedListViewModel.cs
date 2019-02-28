using MvvmCross.IoC;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;
using System.Threading.Tasks;

namespace Surrender_20.UWP.ViewModels
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