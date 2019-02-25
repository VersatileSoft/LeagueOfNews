using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
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
        private readonly IMvxMessenger _messenger;

        public ICommand NavigateCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand CheckInternetConnectionCommand { get; set; }
        public ICommand SelectedPageChangedCommand { get; set; }

        public bool IsSurrender { get; set; } //TODO remove
        public bool MenuVisibility { get; set; }

        public NewsCategory SelectedNewsfeedCategory { get; set; }

        public MainPageViewModel(IInternetConnectionService internetConnectionService, IMvxMessenger messenger)
        {
            _internetConnectionService = internetConnectionService;
            _messenger = messenger;


            NavigateCommand = new MvxAsyncCommand<string>((Parameter) =>
            {
                switch (Parameter)
                {
                    case "Home": return NavigateTo(IsSurrender ? NewsCategory.SurrenderHome : NewsCategory.Official);
                    case "PBE": return NavigateTo(NewsCategory.PBE);
                    case "Red Posts": return NavigateTo(NewsCategory.RedPosts);
                    case "Rotations": return NavigateTo(NewsCategory.Rotations);
                    case "Releases": return NavigateTo(NewsCategory.Releases);
                    case "E-Sports": return NavigateTo(NewsCategory.ESports);
                    default: return null;
                }
            });

            SelectedPageChangedCommand = new MvxCommand(SelectedPageChanged);
        }

        public override void ViewCreated()
        {
            base.ViewCreated();

            _messenger.Publish(new InternetCheckMessage(this, () => _internetConnectionService.IsInternetAvailable()));
        }

        private void SelectedPageChanged()
        {
            if (IsSurrender)
            {
                MenuVisibility = true;
                NavigateTo(NewsCategory.SurrenderHome);
            }
            else
            {
                NavigateTo(NewsCategory.Official);
                MenuVisibility = false;
            }
        }

        protected Task NavigateTo(NewsCategory setting)
        {
            Mvx.IoCProvider.Resolve<NewsfeedListViewModel>().Prepare(setting);
            return Task.CompletedTask;
        }

        public class InternetCheckMessage : MvxMessage
        {
            public InternetCheckMessage(object sender, Func<bool> checkFunction) : base(sender)
            {
                Check = checkFunction;
            }

            public Func<bool> Check { get; set; }
        }
    }
}