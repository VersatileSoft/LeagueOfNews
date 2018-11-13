using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : MvxViewModel
    {

        protected IMvxNavigationService _navigationService;
        protected ITabsInitService _tabsInitService;
        private IOperatingSystemService _operatingSystemService;

        private bool _tabsLoaded = false;

        public ICommand NavCommand { get; private set; } //TODO rename to NavigateCommand
        public ICommand RefreshCommand { get; private set; } //TODO add command that forces RSS service to update

        public MainPageViewModel(IMvxNavigationService navigationService, ITabsInitService tabsInitService, IOperatingSystemService operatingSystemService)
        {
            _navigationService = navigationService;
            _tabsInitService = tabsInitService;
            _operatingSystemService = operatingSystemService;

            NavCommand = new MvxAsyncCommand<string>((Parameter) =>
            {

                switch (Parameter)
                {
                    case "Home": return NavigateTo(Setting.Home);
                    case "PBE": return NavigateTo(Setting.PBE);
                    case "Red Posts": return NavigateTo(Setting.RedPosts);
                    case "Rotations": return NavigateTo(Setting.Rotations);
                    case "Releases": return NavigateTo(Setting.Releases);
                    case "E-Sports": return NavigateTo(Setting.ESports);
                    default: return null;
                }
            });
        }

        protected async Task NavigateTo(Setting setting)
        {
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(setting);
            _tabsInitService.TabsLoaded.Invoke(this, EventArgs.Empty);
        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            if (!_tabsLoaded && _operatingSystemService.GetSystemType() == SystemType.Android)
                MvxNotifyTask.Create(async () => await InitializeViewModels());
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.Home);
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.PBE);
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.Releases);
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.RedPosts);
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.Rotations);
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(Setting.ESports);

            _tabsLoaded = true;
            _tabsInitService.TabsLoaded.Invoke(this, EventArgs.Empty);
        }
    }
}
