using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class BaseViewModel<TParameter> : MvxViewModel, IMvxViewModel<TParameter>
    {
        protected IMvxNavigationService _navigationService;

        public BaseViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public virtual void Prepare(TParameter parameter)
        {

        }
    }
}
