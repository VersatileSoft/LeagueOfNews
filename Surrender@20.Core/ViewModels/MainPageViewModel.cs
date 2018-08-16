using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    [DoNotNotify]
    public class NewsfeedNavigationParameter
    {
        public string Title { get; set; }
        public string URL { get; set; }
    }

    public class MainPageViewModel : BaseViewModel
    {
        public string Title { get; set; } = "Home";
        public string OS { get; set; } //TODO move to service and implement on every platfrom in diffrent way

        public ICommand HomeCommand { get; private set; }
        public ICommand PBECommand { get; private set; }
        public ICommand ReleasesCommand { get; private set; }
        public ICommand RedPostsCommand { get; private set; }
        public ICommand RotationsCommand { get; private set; }
        public ICommand EsportsCommand { get; private set; }

        public ICommand NavViewCommand { get; private set; }

        public MainPageViewModel(IMvxNavigationService navigationService, IOperatingSystemService operatingSystemService) :
            base(navigationService)
        {
            if (operatingSystemService.GetSystemType() != SystemType.Android)
            {
                //FIXME simplfy with new function + struct/parameter class
                HomeCommand = new MvxAsyncCommand(() => NavigateToList(
                    new NewsfeedNavigationParameter { Title = "Home", URL = "url" } ));

                PBECommand = new MvxAsyncCommand(() => NavigateToList(
                    new NewsfeedNavigationParameter { Title = "PBE", URL = "url" } ));

                ReleasesCommand = new MvxAsyncCommand(() => NavigateToList(
                    new NewsfeedNavigationParameter { Title = "Red Posts", URL = "url" }));

                RedPostsCommand = new MvxAsyncCommand(() => NavigateToList(
                    new NewsfeedNavigationParameter { Title = "People", URL = "url" } ));

                RotationsCommand = new MvxAsyncCommand(() => NavigateToList(
                    new NewsfeedNavigationParameter { Title = "E-Sports", URL = "url" }));

                EsportsCommand = new MvxAsyncCommand(() => NavigateToList(
                    new NewsfeedNavigationParameter { Title = "Settings", URL = "url" } ));
            }            
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            if (OS == "Android")
                MvxNotifyTask.Create(async () => await this.InitializeViewModels());
        }

        private async Task InitializeViewModels()
        {
            await NavigateToList(new NewsfeedNavigationParameter {
                Title = "Home",
                URL = "http://feeds.feedburner.com/surrenderat20/CqWw?format=html" }); //TODO shall we save URL somewhere else?

            await NavigateToList(new NewsfeedNavigationParameter {
                Title = "PBE",
                URL = "url" });

            await NavigateToList(new NewsfeedNavigationParameter { 
                Title = "Red Posts",
                URL = "url" });

            await NavigateToList(new NewsfeedNavigationParameter { 
                Title = "People",
                URL = "url" });

            await NavigateToList(new NewsfeedNavigationParameter {
                Title = "E-Sports",
                URL = "url" });

            await NavigateToList(new NewsfeedNavigationParameter {
                Title = "Settings",
                URL = "url" });
        }

        private async Task NavigateToList(NewsfeedNavigationParameter Parameter)
        {
            await _navigationService.Navigate<NewsfeedListViewModel, NewsfeedNavigationParameter>(Parameter);
        }
    }
}
