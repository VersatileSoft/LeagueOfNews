using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.Service;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LeagueOfNews.Forms.Services
{
    public abstract class FormsSettingsService<T> : AbstractSettingsService<T> where T : WebsiteHistoryData, new()
    {
        public FormsSettingsService() : base()
        {
            SetAppTheme();
        }

        protected void SetAppTheme()
        {
            switch (Theme)
            {
                case ApplicationTheme.Dark:
                    SetDarkTheme();
                    break;
                case ApplicationTheme.Light:
                    SetLightTheme();
                    break;
                default:
                    SetDarkTheme();
                    break;
            }
        }

        private void SetLightTheme()
        {
            Application.Current.Resources["Page"] = Application.Current.Resources["LightPage"];
            Application.Current.Resources["Frame"] = Application.Current.Resources["LightFrame"];
            Application.Current.Resources["Label"] = Application.Current.Resources["LightLabel"];
            Application.Current.Resources["DescriptionLabel"] = Application.Current.Resources["LightDescriptionLabel"];
        }
        private void SetDarkTheme()
        {
            Application.Current.Resources["Page"] = Application.Current.Resources["DarkPage"];
            Application.Current.Resources["Frame"] = Application.Current.Resources["DarkFrame"];
            Application.Current.Resources["Label"] = Application.Current.Resources["DarkLabel"];
            Application.Current.Resources["DescriptionLabel"] = Application.Current.Resources["DarkDescriptionLabel"];
        }
    }

    public class AndroidSettingsService : FormsSettingsService<AndroidWebsiteHistoryData>
    {
        private const string CHECK_NEW_POSTS_FREQUENCY = "CHECK_NEW_POSTS_FREQUENCY";
        private const string THEME = "THEME";
        private const string IS_NOTIFICATIONS_ENABLED = "IS_NOTIFICATIONS_ENABLED";

        public AndroidSettingsService() : base() { }

        public override ApplicationTheme Theme
        {
            get => (ApplicationTheme)Preferences.Get(THEME, (int)ApplicationTheme.Dark);
            set
            {
                Preferences.Set(THEME, (int)value);
                SetAppTheme();
            }
        }
        public override int NewPostCheckFrequency
        {
            get => Preferences.Get(CHECK_NEW_POSTS_FREQUENCY, 2);
            set => Preferences.Set(CHECK_NEW_POSTS_FREQUENCY, value);
        }
        public override bool HasNotificationsEnabled
        {
            get => Preferences.Get(IS_NOTIFICATIONS_ENABLED, true);
            set => Preferences.Set(IS_NOTIFICATIONS_ENABLED, value);
        }
    }

    public class AndroidWebsiteHistoryData : WebsiteHistoryData
    {
        private const string LAST_POST_KEY_SURRENDER = "LAST_POST_KEY_SURRENDER";
        private const string LAST_POST_KEY_OFFICIAL = "LAST_POST_KEY_OFFICIAL";
        private const string LAST_POST_KEY_DEVCORNER = "LAST_POST_KEY_DEVCORNER";

        public override string LastSurrenderPostUrl
        {
            get => Preferences.Get(LAST_POST_KEY_SURRENDER, "");
            set => Preferences.Set(LAST_POST_KEY_SURRENDER, value);
        }

        public override string LastOfficialPostUrl
        {
            get => Preferences.Get(LAST_POST_KEY_OFFICIAL, "");
            set => Preferences.Set(LAST_POST_KEY_OFFICIAL, value);
        }

        public override string LastDevCornerPostUrl
        {
            get => Preferences.Get(LAST_POST_KEY_DEVCORNER, "");
            set => Preferences.Set(LAST_POST_KEY_DEVCORNER, value);
        }
    }
}