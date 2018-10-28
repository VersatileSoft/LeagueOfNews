using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : MvxViewModel
    {
        private IOperatingSystemService _operatingSystemService;
        protected IMvxNavigationService _navigationService;

        public string Title { get; set; }

        public ICommand NavCommand { get; private set; } //TODO rename to NavigateCommand
        public ICommand RefreshCommand { get; private set; } //TODO add command that forces RSS service to update

        public MainPageViewModel(IMvxNavigationService navigationService, IOperatingSystemService operatingSystemService)
        {
            _navigationService = navigationService;
            _operatingSystemService = operatingSystemService;

            Title = "Home";

            NavCommand = new MvxAsyncCommand<string>((Parameter) => 
            {
                Title = Parameter;

                switch (Parameter)
                {
                    case "Home": return NavigateTo(Setting.Home);
                    case "PBE": return NavigateTo(Setting.PBE);
                    case "Releases": return NavigateTo(Setting.Releases);
                    case "Red Posts": return NavigateTo(Setting.RedPosts);
                    case "Rotations": return NavigateTo(Setting.Rotations);
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
