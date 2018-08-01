using MvvmCross.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.ViewModels
{
    public class PBEViewModel : BaseViewModel<string>
    {
        public string Title { get; set; } = "PBE";

        public PBEViewModel(IMvxNavigationService navigationService) :
           base(navigationService)
        {

        }
    }
}
