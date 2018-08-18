using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using Surrender_20.Forms.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Surrender_20.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MasterViewModel : MvxViewModel
    {
        public ICommand NavigateCommand { get; set; }

        public MasterViewModel(IMasterDetailService masterDetailService)
        {
            NavigateCommand = new MvxCommand<string>((Parameter) => 
            {
                masterDetailService.MasterPageSelect(Parameter);
            });
        }
    }
}
