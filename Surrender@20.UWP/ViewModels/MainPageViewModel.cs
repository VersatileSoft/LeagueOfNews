using MvvmCross.Commands;
using MvvmCross.Navigation;
using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.UWP.ViewModels
{

    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : MainPageCoreViewModel
    {

        public ICommand NavCommand { get; set; } //TODO rename to NavigateCommand
        public ICommand RefreshCommand { get; set; } //TODO add command that forces RSS service to update

        public MainPageViewModel(IMvxNavigationService navigationService, IOperatingSystemService operatingSystemService)
            : base(navigationService, operatingSystemService)
        {

            NavCommand = new MvxAsyncCommand<string>((Parameter) =>
            {

                switch (Parameter)
                {
                    case "Home": return NavigateTo(Setting.Home);
                    case "PBE": return NavigateTo(Setting.PBE);
                    case "Red Posts": return NavigateTo(Setting.RedPosts);
                    case "Rotations": return NavigateTo(Setting.Rotations);
                    case "Releases": return NavigateTo(Setting.Releases);
                    case "E-Sports": return NavigateTo(Setting.ESports);
                    default: return null;
                }
            });
        }

        protected async Task NavigateTo(Setting setting)
        {
            await _navigationService.Navigate<NewsfeedListViewModel, Setting>(setting);
        }
    }
}
