using MvvmCross.Commands;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Forms.Interfaces;
using System.Windows.Input;

namespace Surrender_20.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MasterViewModel : MvxViewModel
    {
        public string Title { get; set; } = "Menu";

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
