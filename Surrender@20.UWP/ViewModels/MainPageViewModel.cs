using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surrender_20.UWP.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : MvxViewModel
    {
        private readonly IInternetConnectionService _internetConnectionService;

        public ICommand NavigateCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand CheckInternetConnectionCommand { get; set; }
        public ICommand SelectWebsiteCommand { get; set; }

        public bool HasSurrenderElementsVisible { get; set; }

        public NewsWebsite SelectedWebsite { get; set; }
        public NewsCategory SelectedCategory { get; set; }

        public MainPageViewModel(IInternetConnectionService internetConnectionService)
        {
            _internetConnectionService = internetConnectionService;

            NavigateCommand = new MvxAsyncCommand<string>((Parameter) =>
            {
                switch (Parameter)
                {
                    case "Home":
                        switch (SelectedWebsite)
                        {
                            case NewsWebsite.LoL: return NavigateTo(NewsCategory.Official);
                            case NewsWebsite.Surrender: return NavigateTo(NewsCategory.SurrenderHome);
                            default: return Task.CompletedTask;
                        }
                    case "PBE": return NavigateTo(NewsCategory.PBE);
                    case "Red Posts": return NavigateTo(NewsCategory.RedPosts);
                    case "Rotations": return NavigateTo(NewsCategory.Rotations);
                    case "Releases": return NavigateTo(NewsCategory.Releases);
                    case "E-Sports": return NavigateTo(NewsCategory.ESports);
                    default: return Task.CompletedTask;
                }
            });

            SelectWebsiteCommand = new MvxCommand(WebsiteSelected);
        }

        public bool CheckInternetConnection()
        {
            return _internetConnectionService.IsInternetAvailable();
        }

        private void WebsiteSelected()
        {
            switch (SelectedWebsite)
            {
                case NewsWebsite.LoL:
                    HasSurrenderElementsVisible = false;
                    NavigateTo(NewsCategory.SurrenderHome);
                    break;
                case NewsWebsite.Surrender:
                    HasSurrenderElementsVisible = true;
                    NavigateTo(NewsCategory.SurrenderHome);
                    break;
            }
        }

        public override void ViewCreated()
        {
            base.ViewCreated();
        }

        protected Task NavigateTo(NewsCategory setting)
        {
            Mvx.IoCProvider.Resolve<NewsfeedListViewModel>().Prepare(setting);
            return Task.CompletedTask;
        }
    }
}