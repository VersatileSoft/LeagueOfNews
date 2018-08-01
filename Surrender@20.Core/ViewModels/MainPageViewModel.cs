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
            await _navigationService.Navigate<MenuViewModel>();
            await _navigationService.Navigate<NewsfeedListViewModel>();
            
        }        
    }  
}
