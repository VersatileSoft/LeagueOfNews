using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModels
{
    public class MainPageViewModel : BaseViewModel<string>
    {     
        public string Title { get; set; } = "Home";
        public bool IsPresented { get; private set; } = true;

        public MainPageViewModel(IMvxNavigationService navigationService) : 
            base(navigationService)
        {

        }

        public override void ViewAppearing()
        {
            base.ViewAppearing();

            MvxNotifyTask.Create(async () => await this.InitializeViewModels());
        }

        private async Task InitializeViewModels()
        {
            await _navigationService.Navigate<NewsfeedListViewModel, string>("Home");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("PBE");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("Releases");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("Red Posts");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("People");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("E-Sports");
            await _navigationService.Navigate<NewsfeedListViewModel, string>("Settings");
            
        }        
    }  
}
