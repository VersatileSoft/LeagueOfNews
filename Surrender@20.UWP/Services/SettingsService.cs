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

        public SettingsService() : base()
        {
            this.TitleChanged += OnTitleChanged;

            foreach (var category in categories)
            {
                category.Value.LastPostTitle = _localSettings.Values.TryGetValue(category.Key.ToString(), out var value) ? value as string : null;
            }
        }

        private void OnTitleChanged(PostTitleArgs args)
        {
            _localSettings.Values[args.Category.ToString()] = args.Title;
        }
    }
}
