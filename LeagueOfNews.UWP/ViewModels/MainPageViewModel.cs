using LeagueOfNews.Core.Interface;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using PropertyChanged;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeagueOfNews.UWP.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel : MvxViewModel
    {
        private readonly IInternetConnectionService _internetConnectionService;
        private readonly ISettingsService _settingsService;

        public ICommand SelectItemCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand CheckInternetConnectionCommand { get; set; }
        public ICommand SelectWebsiteCommand { get; set; }
        public ICommand SelectThemeCommand { get; set; }

        public Core.Interface.ApplicationTheme DefaultTheme { get; set; }

        public bool HasSurrenderElementsVisible { get; set; }
        public NewsWebsite SelectedWebsite { get; set; }

        public MainPageViewModel(
            IInternetConnectionService internetConnectionService,
            ISettingsService settingsService)
        {
            _internetConnectionService = internetConnectionService;
            _settingsService = settingsService;

            DefaultTheme = settingsService.Theme;
            SelectedWebsite = NewsWebsite.Surrender;
            HasSurrenderElementsVisible = true;

            SelectItemCommand = new MvxAsyncCommand<dynamic>((Parameter) =>
            {
                switch (Parameter.SelectedItem.Content)
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

            SelectThemeCommand = new MvxCommand<RoutedEventArgs>((Parameter) =>
            {
                switch ((Parameter.OriginalSource as RadioButton).Tag)
                {
                    case "Dark":
                        _settingsService.Theme = Core.Interface.ApplicationTheme.Dark;
                        break;
                    case "Light":
                        _settingsService.Theme = Core.Interface.ApplicationTheme.Light;
                        break;
                    default:
                        _settingsService.Theme = Core.Interface.ApplicationTheme.Default;
                        break;
                }
            });

            SelectWebsiteCommand = new MvxCommand<SelectionChangedEventArgs>((Parameter) =>
            {
                switch (Parameter.AddedItems[0].ToString())
                {
                    case "League of Legends official":
                        HasSurrenderElementsVisible = false;
                        SelectedWebsite = NewsWebsite.Surrender;
                        NavigateTo(NewsCategory.Official);
                        break;
                    case "Surrender@20":
                        HasSurrenderElementsVisible = true;
                        SelectedWebsite = NewsWebsite.LoL;
                        NavigateTo(NewsCategory.SurrenderHome);
                        break;
                }
            });
        }

        public override void ViewCreated()
        {
            //SelectItemCommand.Execute(new { SelectedItem = new { Content = "Home" } });
        }

        public bool CheckInternetConnection()
        {
            return _internetConnectionService.IsInternetAvailable();
        }

        protected Task NavigateTo(NewsCategory setting)
        {
            Mvx.IoCProvider.Resolve<NewsfeedListViewModel>().Prepare(setting);
            return Task.CompletedTask;
        }
    }
}