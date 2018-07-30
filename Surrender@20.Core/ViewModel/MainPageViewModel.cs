using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;

namespace Surrender_20.Core.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel
    {
        public string Title { get; set; } = "Weź coś zrób";

        public DelegateCommand MenuItemTapped { get; set; }

        public void InitCommands()
        {
            MenuItemTapped = new DelegateCommand(MenuItemTapped);
        }

        private void MenuItemTapped()
        {
            //do something when clicked
        }
    }  
}
