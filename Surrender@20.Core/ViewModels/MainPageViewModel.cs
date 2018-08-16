using MvvmCross.Commands;
using MvvmCross.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : MvxViewModel
    {
        private IOperatingSystemService _operatingSystemService;
        private IMvxNavigationService _navigationService;

        public string Title { get; set; } = "Home";

        public ICommand HomeCommand { get; private set; }
        public ICommand PBECommand { get; private set; }
        public ICommand ReleasesCommand { get; private set; }
        public ICommand RedPostsCommand { get; private set; }
        public ICommand RotationsCommand { get; private set; }
        public ICommand EsportsCommand { get; private set; }

        public MainPageViewModel(IMvxNavigationService navigationService, IOperatingSystemService operatingSystemService, IMvxLog log)
        {
            _navigationService = navigationService;
            _operatingSystemService = operatingSystemService;

            HomeCommand = new MvxAsyncCommand(() => NavigateTo(Setting.Home));
            PBECommand = new MvxAsyncCommand(() => NavigateTo(Setting.PBE));
            ReleasesCommand = new MvxAsyncCommand(() => NavigateTo(Setting.Releases));
            RedPostsCommand = new MvxAsyncCommand(() => NavigateTo(Setting.RedPosts));
            RotationsCommand = new MvxAsyncCommand(() => NavigateTo(Setting.People));
            EsportsCommand = new MvxAsyncCommand(() => NavigateTo(Setting.ESports));
        }

        public async override void ViewAppearing()
        {
            base.ViewAppearing();

            if (_operatingSystemService.GetSystemType() == SystemType.Android)
            {
                await NavigateTo(Setting.Home);
                await NavigateTo(Setting.PBE);
                //await NavigateTo(Setting.Releases);
                //await NavigateTo(Setting.RedPosts);
                //await NavigateTo(Setting.People);
                //await NavigateTo(Setting.ESports);
            }
        }

        private async Task NavigateTo(Setting setting)
        {
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(setting);
        }
    }
}
