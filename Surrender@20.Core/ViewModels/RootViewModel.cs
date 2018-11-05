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
    public class RootViewModel : MvxViewModel
    {

        private IMvxNavigationService _navigationService;
        private IOperatingSystemService _operatingSystemService;
        private ITabsInitService _tabsInitService;

        public ICommand NavCommand { get; private set; } //TODO rename to NavigateCommand
        public ICommand RefreshCommand { get; private set; } //TODO add command that forces RSS service to update

        public RootViewModel(IMvxNavigationService navigationService, 
            IOperatingSystemService operatingSystemService, ITabsInitService tabsInitService)
        {
            _navigationService = navigationService;
            _operatingSystemService = operatingSystemService;
            _tabsInitService = tabsInitService;
            

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

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            if (_operatingSystemService.GetSystemType() == SystemType.Android)
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

            _tabsInitService.TabsLoaded.Invoke(this, EventArgs.Empty);
        }

        protected async Task NavigateTo(Setting setting)
        {
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(setting);
        }
    }
}
