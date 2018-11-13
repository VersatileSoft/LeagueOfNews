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


        public ICommand NavCommand { get; private set; } //TODO rename to NavigateCommand
        public ICommand RefreshCommand { get; private set; } //TODO add command that forces RSS service to update

        public MainPageViewModel(IMvxNavigationService navigationService, ITabsInitService tabsInitService)
        {
            _navigationService = navigationService;
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

        protected async Task NavigateTo(Setting setting)
        {
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(setting);
            _tabsInitService.TabsLoaded.Invoke(this, EventArgs.Empty);
        }
    }
}
