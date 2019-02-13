using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Model;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.UWP.ViewModels
{
    public class MainPageViewModel : MainPageCoreViewModel
    {
        private IInternetConnectionService _internetConnectionService;
        private Pages _selectedNewsfeedCategory;
        private Newsfeed _selectedNewsfeed;

        public ICommand NavigateCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand CheckInternetConnectionCommand { get; set; }

        public MvxInteraction<Func<bool>> CheckInternetConnectionInteraction { get; }

        public Newsfeed SelectedNewsfeed {
            get => _selectedNewsfeed;
            set => SetProperty(ref _selectedNewsfeed, value);
        }

        public Pages SelectedNewsfeedCategory 
        {
            get => _selectedNewsfeedCategory;
            set => SetProperty(ref _selectedNewsfeedCategory, value);
        }

        public MainPageViewModel(
            IMvxNavigationService navigationService, 
            IOperatingSystemService operatingSystemService,
            IInternetConnectionService internetConnectionService)
                : base(navigationService, operatingSystemService)
        {
            this._internetConnectionService = internetConnectionService;
            
            this.CheckInternetConnectionInteraction = new MvxInteraction<Func<bool>>();

            NavigateCommand = new MvxAsyncCommand<string>((Parameter) =>
            {
                switch (Parameter)
                {
                    case "Home": return NavigateTo(Pages.SurrenderHome);
                    case "PBE": return NavigateTo(Pages.PBE);
                    case "Red Posts": return NavigateTo(Pages.RedPosts);
                    case "Rotations": return NavigateTo(Pages.Rotations);
                    case "Releases": return NavigateTo(Pages.Releases);
                    case "E-Sports": return NavigateTo(Pages.ESports);
                    default: return null;
                }
            });
        }

        public override void ViewCreated()
        {
            base.ViewCreated();

            this.CheckInternetConnectionInteraction.Raise(() => _internetConnectionService.IsInternetAvailable());
        }

        protected Task NavigateTo(Pages setting)
        {
            SelectedNewsfeedCategory = setting;
            return Task.CompletedTask;
        }
    }
}