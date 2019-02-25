using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Core.Interface;
using System.Text.RegularExpressions;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsCoreViewModel : MvxViewModel
    {
        public bool IsNotificationsEnabled
        {
            get => _settingsService.HasNotificationsEnabled;
            set
            {
                _settingsService.HasNotificationsEnabled = value;
                _notificationService.RefreshNotificationJobService();
            }
        }

        public bool DarkTheme
        {
            get => _settingsService.Theme == ApplicationTheme.Dark;
            set => _settingsService.Theme = value ? ApplicationTheme.Dark : ApplicationTheme.Light;
        }

        public string Delay
        {
            get => _settingsService.NewPostCheckFrequency + " Hours";
            set
            {
                _settingsService.NewPostCheckFrequency = int.Parse(Regex.Match(value, @"\d+").Value);
                _notificationService.RefreshNotificationJobService();
            }
        }

        public MvxObservableCollection<string> DelayList { get; set; }

        private readonly ISettingsService _settingsService;
        private readonly INotificationService _notificationService;

        public SettingsCoreViewModel(ISettingsService settingsService, INotificationService notificationService)
        {
            _settingsService = settingsService;
            _notificationService = notificationService;
            DelayList = new MvxObservableCollection<string>
            {
                "2 Hours",
                "6 Hours",
                "12 Hours",
                "24 Hours"
            };
        }
    }
}