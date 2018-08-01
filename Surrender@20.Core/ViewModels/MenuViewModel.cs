using MvvmCross.ViewModels;
using PropertyChanged;
using MvvmCross.Commands;
using System.Windows.Input;
using MvvmCross.Navigation;
using static System.Net.Mime.MediaTypeNames;

namespace Surrender_20.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel<string>
    {
        public string Title { get; set; } = "Home";

        public ICommand HomeCommand { get; private set; }
        public ICommand PBECommand { get; private set; }
        public ICommand ReleasesCommand { get; private set; }
        public ICommand RedPostsCommand { get; private set; }
        public ICommand RotationsCommand { get; private set; }
        public ICommand EsportsCommand { get; private set; }

        public MenuViewModel(IMvxNavigationService navigationService) :
            base(navigationService)
        {
            

            HomeCommand = new MvxCommand(() => navigationService.Navigate<NewsfeedListViewModel, string>(""));
            PBECommand = new MvxCommand(() => navigationService.Navigate<PBEViewModel, string>("nie wiem czy zrobić BaseViewModel bez generyka, w sumie nie chce mi się xD"));
            ReleasesCommand = new MvxCommand(Placeholder);
            RedPostsCommand = new MvxCommand(Placeholder);
            RotationsCommand = new MvxCommand(Placeholder);
            EsportsCommand = new MvxCommand(Placeholder);
        }

        void Placeholder()
        {

        }
    }
}
