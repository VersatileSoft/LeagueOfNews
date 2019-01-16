using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Surrender_20.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surrender_20.Core.ViewModels.Android
{
    public class MenuViewModel : MvxViewModel
    {

        private readonly IMvxNavigationService _navigationService;

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public IMvxCommand<Pages> NavigateCommand
        {
            get { return new MvxAsyncCommand<Pages>(NavigateExecuted); }
        }

        private async Task NavigateExecuted(Pages page)
        {
            if(page == Pages.SurrenderHome)
                await _navigationService.Navigate<TabbedRootViewModel>();
            else
                await _navigationService.Navigate<NewsfeedListCoreViewModel>();

        }
    }
}
