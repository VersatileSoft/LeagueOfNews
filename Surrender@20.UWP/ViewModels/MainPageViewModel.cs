using MvvmCross.Navigation;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.UWP.ViewModels
{
    public class MainPageViewModel : MainPageCoreViewModel
    {
        public MainPageViewModel(IMvxNavigationService navigationService, ITabsInitService tabsInitService, IOperatingSystemService operatingSystemService) 
            : base(navigationService, tabsInitService, operatingSystemService)
        {
        }
    }
}
