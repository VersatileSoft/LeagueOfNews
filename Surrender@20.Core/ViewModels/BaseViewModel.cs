using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.ViewModels
{ 
    //TODO remove, we don't really need that class
    public class BaseViewModel : MvxViewModel<MvxBundle>
    {
        protected IMvxNavigationService _navigationService;

        public BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Prepare(MvxBundle parameter)
        {
            throw new NotImplementedException();
        }
    }
}
