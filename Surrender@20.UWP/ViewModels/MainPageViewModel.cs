using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace Surrender_20.UWP.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : MvxViewModel
    {
        private readonly IInternetConnectionService _internetConnectionService;

        public ICommand NavigateCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand CheckInternetConnectionCommand { get; set; }
        public ICommand SelectedPageChangedCommand { get; set; }

        public bool IsSurrender { get; set; }
        public Visibility MenuVisible { get; set; }
        
        public MvxInteraction<Func<bool>> CheckInternetConnectionInteraction { get; }

        public Pages SelectedNewsfeedCategory { get; set; }

        public MainPageViewModel(IInternetConnectionService internetConnectionService)
        {
            _internetConnectionService = internetConnectionService;

            CheckInternetConnectionInteraction = new MvxInteraction<Func<bool>>();

            NavigateCommand = new MvxAsyncCommand<string>((Parameter) =>
            {
                switch (Parameter)
                {
                    case "Home": return NavigateTo(IsSurrender ? Pages.SurrenderHome : Pages.Official);
                    case "PBE": return NavigateTo(Pages.PBE);
                    case "Red Posts": return NavigateTo(Pages.RedPosts);
                    case "Rotations": return NavigateTo(Pages.Rotations);
                    case "Releases": return NavigateTo(Pages.Releases);
                    case "E-Sports": return NavigateTo(Pages.ESports);
                    default: return null;
                }
            });

            SelectedPageChangedCommand = new MvxCommand(SelectedPageChanged);
        }

        private void SelectedPageChanged()
        {
            if (IsSurrender)
            {
                MenuVisible = Visibility.Visible;
            }
            else
                MenuVisible = Visibility.Collapsed;
        }

        public override void ViewCreated()
        {
            base.ViewCreated();

            CheckInternetConnectionInteraction.Raise(() => _internetConnectionService.IsInternetAvailable());
        }

        protected Task NavigateTo(Pages setting)
        {
            Mvx.IoCProvider.Resolve<NewsfeedListViewModel>().Prepare(setting);
            return Task.CompletedTask;
        }
    }
}