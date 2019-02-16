using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage;
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
        public bool MenuVisibility { get; set; }

        public MvxInteraction<Func<bool>> CheckInternetConnectionInteraction { get; }

        public NewsCategory SelectedNewsfeedCategory { get; set; }

        public MainPageViewModel(IInternetConnectionService internetConnectionService)
        {
            _internetConnectionService = internetConnectionService;

            CheckInternetConnectionInteraction = new MvxInteraction<Func<bool>>();

            NavigateCommand = new MvxAsyncCommand<string>((Parameter) =>
            {
                switch (Parameter)
                {
                    case "Home": return NavigateTo(IsSurrender ? NewsCategory.SurrenderHome : NewsCategory.Official);
                    case "PBE": return NavigateTo(NewsCategory.PBE);
                    case "Red Posts": return NavigateTo(NewsCategory.RedPosts);
                    case "Rotations": return NavigateTo(NewsCategory.Rotations);
                    case "Releases": return NavigateTo(NewsCategory.Releases);
                    case "E-Sports": return NavigateTo(NewsCategory.ESports);
                    default: return null;
                }
            });

            SelectedPageChangedCommand = new MvxCommand(SelectedPageChanged);
        }

        private void SelectedPageChanged()
        {
            if (IsSurrender)
            {
                MenuVisibility = true;
                NavigateTo(NewsCategory.SurrenderHome);
            }
            else
            {
                NavigateTo(NewsCategory.Official);
                MenuVisibility = false;
            }
        }

        public void LoadSettings() 
        {
            if (_localSettings.Values.TryGetValue("Theme", out var value))
            {
                SelectedTheme = (ApplicationTheme) value;
            }
        }

        private void SelectedThemeChanged()
        {
            if (IsLight)
            {
                _localSettings.Values["Theme"] = ApplicationTheme.Light;
            }
            else if (IsDark)
            {
                _localSettings.Values["Theme"] = ApplicationTheme.Dark;
            }
            else if (IsUsingDefaultColors)
            {
                ApplicationData.Current.LocalSettings.Values.Remove("Theme");
            }
        }

        public override void ViewCreated()
        {
            base.ViewCreated();

            CheckInternetConnectionInteraction.Raise(() => _internetConnectionService.IsInternetAvailable());
        }

        protected Task NavigateTo(NewsCategory setting)
        {
            Mvx.IoCProvider.Resolve<NewsfeedListViewModel>().Prepare(setting);
            return Task.CompletedTask;
        }
    }
}