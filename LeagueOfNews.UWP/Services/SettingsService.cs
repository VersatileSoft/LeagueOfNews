using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.Service;
using System;
using Windows.Storage;

namespace LeagueOfNews.UWP.Services
{
    public class SettingsService : AbstractSettingsService
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
            get => (_localSettings.Values.TryGetValue("Frequency", out object value)) ? (int)value : -1;
            set => _localSettings.Values["Frequency"] = value;
        }

        public override bool HasNotificationsEnabled
        {
            get => (_localSettings.Values.TryGetValue("Notifications", out object value)) ? (bool)value : true;
            set => _localSettings.Values["Notifications"] = value;
        }

        public override WebsiteHistoryData WebsiteHistoryData => throw new NotImplementedException();

        public SettingsService() : base()
        {
            //TODO Change to work with new core code :D

            //TitleChanged += OnTitleChanged;

            ////TODO use consts like in Android project
            //WebsiteHistoryData.LastOfficialPostUrl = _localSettings.Values
            //    .TryGetValue("LastOfficialPostUrl", out object official) ? official as string : "";

            //WebsiteHistoryData.LastSurrenderPostUrl = _localSettings.Values
            //    .TryGetValue("LastSurrenderPostUrl", out object surrender) ? surrender as string : "";
        }

        //private void OnTitleChanged(PostTitleArgs args)
        //{
        //    switch (args.Category)
        //    {
        //        case NewsWebsite.LoL:
        //            _localSettings.Values["LastOfficialPostUrl"] = args.Title;
        //            break;
        //        case NewsWebsite.Surrender:
        //            _localSettings.Values["LastSurrenderPostUrl"] = args.Title;
        //            break;
        //    }
        //}
    }
}