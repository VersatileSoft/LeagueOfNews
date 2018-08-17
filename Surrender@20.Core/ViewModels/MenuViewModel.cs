using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MenuViewModel : MvxViewModel
    {
        public string Title { get; set; }
        public ICommand NavCommand { get; private set; }
        private IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            Title = "Menu";
            _navigationService = NavigationService;
            NavCommand = new MvxAsyncCommand<string>(async (Parameter) =>
            {
                switch (Parameter) //duplicated code in main page vm, to fix, idk how
                {
                    case "Home": await NavigateTo(Setting.Home); Title = "Home"; break;
                    case "PBE": await NavigateTo(Setting.PBE); Title = "Public Beta Environment"; break;
                    case "Releases": await NavigateTo(Setting.Releases); Title = "Releases"; break;
                    case "Red Posts": await NavigateTo(Setting.RedPosts); Title = "Red Posts"; break;
                    case "Rotations": await NavigateTo(Setting.People); Title = "Rotations"; break;
                    case "E-Sports": await NavigateTo(Setting.ESports); Title = "E-Sports"; break;
                    default: break;
                }
            });
          
        }

        private async Task NavigateTo(Setting setting)
        {
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(setting);
        }
    }
}
