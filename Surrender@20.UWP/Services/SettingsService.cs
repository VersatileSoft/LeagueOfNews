using Surrender_20.Core.Interface;
using Surrender_20.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Surrender_20.UWP.Services
{
    public class SettingsService : AbstractSettingsService
    {
        private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        public override ApplicationTheme Theme {
            get => (_localSettings.Values.TryGetValue("Theme", out var value)) ? (ApplicationTheme) value : ApplicationTheme.Default;
            set => _localSettings.Values["Theme"] = value;
        }

        public override int NewPostCheckFrequency {
            get => (_localSettings.Values.TryGetValue("Frequency", out var value)) ? (int) value : -1;
            set => _localSettings.Values["Frequency"] = value;
        }

        public override bool HasNotificationsEnabled {
            get => (_localSettings.Values.TryGetValue("Notifications", out var value)) ? (bool) value : true;
            set => _localSettings.Values["Notifications"] = value;
        }

        public void LoadSettings()
        {
            if (_localSettings.Values.TryGetValue("Theme", out var value))
            {
                SelectedTheme = (ApplicationTheme)value;
            }
        }

        private void SelectedThemeChanged()
        {
            if (IsLight)
            {
                _localSettings.Values["Theme"] = ApplicationTheme.Light;
            }
            else if (IsDark)
            {
                _localSettings.Values["Theme"] = ApplicationTheme.Dark;
            }
            else if (IsUsingDefaultColors)
            {
                ApplicationData.Current.LocalSettings.Values.Remove("Theme");
            }
        }
    }
}
