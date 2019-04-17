using Android.App;
using Android.Preferences;
using LeagueOfNews.Core.Interface;
using LeagueOfNews.Forms.Services;

namespace LeagueOfNews.Forms.Droid.Services
{
    public class AndroidSettingsService : FormsSettingsService
    {

        private const string LAST_POST_KEY_SURRENDER = "LAST_POST_KEY_SURRENDER";
        private const string LAST_POST_KEY_OFFICIAL = "LAST_POST_KEY_OFFICIAL";
        private const string CHECK_NEW_POSTS_FREQUENCY = "CHECK_NEW_POSTS_FREQUENCY";
        private const string THEME = "THEME";
        private const string IS_NOTIFICATIONS_ENABLED = "IS_NOTIFICATIONS_ENABLED";

        public AndroidSettingsService() : base()
        {
            WebsiteHistoryData = new AndroidWebsiteHistoryData();
        }

        public override ApplicationTheme Theme
        {
            get => (ApplicationTheme)PreferenceManager.GetDefaultSharedPreferences(Application.Context).GetInt(THEME, (int)ApplicationTheme.Dark);
            set
            {
                PreferenceManager.GetDefaultSharedPreferences(Application.Context)
                    .Edit()
                    .PutInt(THEME, (int)value)
                    .Apply();
                SetAppTheme();
            }
        }
        public override int NewPostCheckFrequency
        {
            get => PreferenceManager.GetDefaultSharedPreferences(Application.Context).GetInt(CHECK_NEW_POSTS_FREQUENCY, 2);
            set => PreferenceManager.GetDefaultSharedPreferences(Application.Context)
                .Edit()
                .PutInt(CHECK_NEW_POSTS_FREQUENCY, value)
                .Apply();
        }
        public override bool HasNotificationsEnabled
        {
            get => PreferenceManager.GetDefaultSharedPreferences(Application.Context).GetBoolean(IS_NOTIFICATIONS_ENABLED, true);
            set => PreferenceManager.GetDefaultSharedPreferences(Application.Context)
                    .Edit()
                    .PutBoolean(IS_NOTIFICATIONS_ENABLED, value)
                    .Apply();
        }

        public class AndroidWebsiteHistoryData : WebsiteHistoryData
        {

            public override string LastSurrenderPostUrl
            {
                get => PreferenceManager.GetDefaultSharedPreferences(Application.Context).GetString(LAST_POST_KEY_SURRENDER, "");
                set => PreferenceManager.GetDefaultSharedPreferences(Application.Context)
                 .Edit()
                 .PutString(LAST_POST_KEY_SURRENDER, value)
                 .Apply();
            }

            public override string LastOfficialPostUrl
            {
                get => PreferenceManager.GetDefaultSharedPreferences(Application.Context).GetString(LAST_POST_KEY_OFFICIAL, "");
                set => PreferenceManager.GetDefaultSharedPreferences(Application.Context)
                 .Edit()
                 .PutString(LAST_POST_KEY_OFFICIAL, value)
                 .Apply();
            }
        }
    }
}