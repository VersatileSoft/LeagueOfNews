using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Surrender_20.Core.ViewModels.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.ViewModels
{
    public class MainViewModel : MvxViewModel
    {

        private readonly IMvxNavigationService _navigationService;

        public MainViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void ShowMenu()
        {
            MvxNotifyTask.Create(async () =>
            {
                await _navigationService.Navigate<NewsfeedListViewModel>(); 
                await _navigationService.Navigate<MenuViewModel>();
            });
        }

        public void ShowHome()
        {
            MvxNotifyTask.Create(async () =>
            {
                await _navigationService.Navigate<NewsfeedListViewModel>();
            });
        }

        public void Init(object hint)
        {
            // Can perform Vm data retrival here based on any
            // data passed in the hint object

            // ShowViewModel<SomeViewModel>(new { derp: "herp", durr: "derrrrrr" });
            // public class SomeViewModel : MvxViewModel
            // {
            //     public void Init(string derp, string durr)
            //     {
            //     }
            // }
        }

        public override void Start()
        {
            //base.Start();
        }
    }
}
