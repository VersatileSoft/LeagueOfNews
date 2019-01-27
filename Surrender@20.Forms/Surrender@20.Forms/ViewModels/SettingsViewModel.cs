using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;

namespace Surrender_20.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsViewModel : SettingsCoreViewModel
    {
        public SettingsViewModel(ISaveDataService saveDataService, INotificationService notificationService, IThemeService themeService) : base(saveDataService, notificationService, themeService)
        {
        }
    }
}