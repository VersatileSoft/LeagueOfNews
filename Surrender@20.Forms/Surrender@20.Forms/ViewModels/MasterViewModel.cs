using MvvmCross.Commands;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Forms.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Surrender_20.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MasterViewModel : MvxViewModel
    {
        public string Title { get; set; } = "Menu";

        public ICommand NavigateCommand { get; set; }

        public ObservableCollection<MenuListElement> MenuElements { get; set; }

        public MasterViewModel(IMasterDetailService masterDetailService)
        {
            MenuElements = new ObservableCollection<MenuListElement>
            {
                new MenuListElement { Name = "League of Legends Official", MenuImage = "LolOfficialIcon.png", Page = Pages.Official },
                //new MenuListElement { Name = "/dev blog", MenuImage = "DevBlogIcon.png", Page = Pages.Dev }, //TODO zmiana Page
                new MenuListElement { Name = "Surrender@20", MenuImage = "SurrenderAt20Icon.png", Page = Pages.SurrenderHome },
                new MenuListElement { Name = "Settings", MenuImage = "SettingsIcon.png", Page = Pages.Settings }
            };

            NavigateCommand = new MvxCommand<MenuListElement>((Parameter) =>
            {
                masterDetailService.MasterPageSelect(Parameter.Page);
            });
        }
    }

    public class MenuListElement
    {
        public Pages Page { get; set; }
        public string Name { get; set; }
        public string MenuImage { get; set; }
    }
}