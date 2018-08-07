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

        public MainPageViewModel(IMvxNavigationService navigationService) :
            base(navigationService)
        {
            if (OS != "Android")
            {
                HomeCommand = new MvxAsyncCommand(() => NavigateToList(Setting.Home));
                PBECommand = new MvxAsyncCommand(() => NavigateToList(Setting.Home));
                ReleasesCommand = new MvxAsyncCommand(() => NavigateToList(Setting.Home));
                RedPostsCommand = new MvxAsyncCommand(() => NavigateToList(Setting.Home));
                RotationsCommand = new MvxAsyncCommand(() => NavigateToList(Setting.Home));
                EsportsCommand = new MvxAsyncCommand(() => NavigateToList(Setting.Home));
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
            await NavigateToList(Setting.Home);
            await NavigateToList(Setting.PBE); 
            await NavigateToList(Setting.RedPosts); 
            await NavigateToList(Setting.People); 
            await NavigateToList(Setting.ESports); 
        }

        private async Task NavigateToList(Setting setting)
        {
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(setting);
        }
    }
}
