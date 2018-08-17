using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : MvxViewModel
    {
        private IOperatingSystemService _operatingSystemService;
        private IMvxNavigationService _navigationService;

        public string Title { get; set; }

        public ICommand HomeCommand { get; private set; }
        public ICommand PBECommand { get; private set; }
        public ICommand ReleasesCommand { get; private set; }
        public ICommand RedPostsCommand { get; private set; }
        public ICommand RotationsCommand { get; private set; }
        public ICommand EsportsCommand { get; private set; }

        public ICommand NavCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; } //halp

        public MainPageViewModel(IMvxNavigationService navigationService, IOperatingSystemService operatingSystemService, IMvxLog log)
        {
            _navigationService = navigationService;
            _operatingSystemService = operatingSystemService;

            Title = "Home";

            NavCommand = new MvxCommand<string>((Parameter) => 
            {
                switch (Parameter) //maybe da sie jakoś inaczej (lepiej?) zrobić to Title
                {
                    case "Home": HomeCommand.Execute(null); Title = "Home"; break;
                    case "PBE": PBECommand.Execute(null); Title = "Public Beta Environment"; break;
                    case "Releases": ReleasesCommand.Execute(null); Title = "Releases"; break;
                    case "Red Posts": RedPostsCommand.Execute(null); Title = "Red Posts"; break;
                    case "Rotations": RotationsCommand.Execute(null); Title = "Rotations"; break;
                    case "E-Sports": EsportsCommand.Execute(null); Title = "E-Sports"; break;
                    default: break;
                }
            });

            HomeCommand = new MvxAsyncCommand(() => NavigateTo(Setting.Home));
            PBECommand = new MvxAsyncCommand(() => NavigateTo(Setting.PBE));
            ReleasesCommand = new MvxAsyncCommand(() => NavigateTo(Setting.Releases));
            RedPostsCommand = new MvxAsyncCommand(() => NavigateTo(Setting.RedPosts));
            RotationsCommand = new MvxAsyncCommand(() => NavigateTo(Setting.People));
            EsportsCommand = new MvxAsyncCommand(() => NavigateTo(Setting.ESports));          
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();
            
             if (_operatingSystemService.GetSystemType() == SystemType.Android)
                  MvxNotifyTask.Create(async () => await this.InitializeViewModels());
           
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<MenuViewModel>();
            await NavigateTo(Setting.Home);
        }

        private async Task NavigateTo(Setting setting)
        {
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(setting);
        }
    }
}
