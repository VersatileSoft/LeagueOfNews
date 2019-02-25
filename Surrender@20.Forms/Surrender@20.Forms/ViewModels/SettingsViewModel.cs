using PropertyChanged;
using Surrender_20.Core.Interface;
using Surrender_20.Core.ViewModels;

namespace Surrender_20.Forms.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsViewModel : SettingsCoreViewModel
    {
        public SettingsViewModel(ISettingsService settingsService, INotificationService notificationService) : base(settingsService, notificationService)
        {
        }
    }
}