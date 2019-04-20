using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;
using static LeagueOfNews.UWP.Services.SettingsService;

namespace LeagueOfNews.UWP.Services
{
    public class SettingsService : AbstractSettingsService<WindowsWebsiteHistoryData>
    {
        private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

        public override ApplicationTheme Theme
        {
            get => (_localSettings.Values.TryGetValue("Theme", out object value))
                ? (ApplicationTheme)Enum.Parse(typeof(ApplicationTheme), value as string)
                : ApplicationTheme.Default;
            set => _localSettings.Values["Theme"] = value.ToString();
        }

        public override int NewPostCheckFrequency
        {
            get => (_localSettings.Values.TryGetValue("Frequency", out object value)) ? (int)value : 15;
            set => _localSettings.Values["Frequency"] = value;
        }

        public override bool HasNotificationsEnabled
        {
            get => (_localSettings.Values.TryGetValue("Notifications", out object value)) ? (bool)value : true;
            set => _localSettings.Values["Notifications"] = value;
        }

        public class WindowsWebsiteHistoryData : WebsiteHistoryData
        {
            private readonly ApplicationDataContainer _settings = ApplicationData.Current.LocalSettings;

            public override string LastSurrenderPostUrl
            {
                get => (_settings.Values.TryGetValue("LastSurrenderPostUrl", out object value)) ? value as string : "";
                set => _settings.Values["LastSurrenderPostUrl"] = value;
            }

            public override string LastOfficialPostUrl
            {
                get => (_settings.Values.TryGetValue("LastOfficialPostUrl", out object value)) ? value as string : "";
                set => _settings.Values["LastOfficialPostUrl"] = value;
            }

            public override List<string> VisitedPosts
            {
                get => (_settings.Values.TryGetValue("VisitedPosts", out object value)) ? new List<string>((value as string).Split("|")) : new List<string>();
                set
                {
                    string output = value.Aggregate((sum, x) => sum += x + "|");
                    _settings.Values["VisitedPosts"] = output.Substring(0, output.Length - 1);
                }
            }
        }
    }
}