using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel
    {     
        public string Title { get; set; } = "Weź coś zrób";

        public ICommand MenuItemTappedCommand { get; private set; }

        public void InitCommands()
        {
            //MenuItemTappedCommand = new Command(MenuItemTapped);
        }

        private void MenuItemTapped()
        {
            //do something when clicked
        }
    }  
}
