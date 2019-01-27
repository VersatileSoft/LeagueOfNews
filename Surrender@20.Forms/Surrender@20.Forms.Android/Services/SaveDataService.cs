using Android.App;
using Android.Content;
using Android.Preferences;
using Surrender_20.Core.Interface;

namespace Surrender_20.Forms.Droid.Services
{
    public class SaveDataService : ISaveDataService
    {

        private const string LAST_POST_KEY_SURRENDER = "LAST_POST_KEY_SURRENDER";
        private const string LAST_POST_KEY_OFFICIAL = "LAST_POST_KEY_OFFICIAL";
        private const string CHECK_NEW_POSTS_FREQUENCY = "CHECK_NEW_POSTS_FREQUENCY";
        private const string IS_DARK_THEME = "IS_DARK_THEME";
        private const string IS_NOTIFICATIONS_ENABLED = "IS_NOTIFICATIONS_ENABLED";

        public int GetCheckNewPostsFrequency()
        {
            return PreferenceManager.GetDefaultSharedPreferences(Application.Context).GetInt(CHECK_NEW_POSTS_FREQUENCY, 2);
        }

        public bool GetIsDarkTheme()
        {
            return PreferenceManager.GetDefaultSharedPreferences(Application.Context).GetBoolean(IS_DARK_THEME, true);
        }

        public bool GetIsNotificationsEnabled()
        {
            return PreferenceManager.GetDefaultSharedPreferences(Application.Context).GetBoolean(IS_NOTIFICATIONS_ENABLED, true);
        }

        public void SaveCheckNewPostsFrequency(int Frequency)
        {
            ISharedPreferencesEditor editor = PreferenceManager.GetDefaultSharedPreferences(Application.Context).Edit();
            editor.PutInt(CHECK_NEW_POSTS_FREQUENCY, Frequency);
            editor.Apply();
        }

        public void SaveIsDarkTheme(bool IsEnabled)
        {
            ISharedPreferencesEditor editor = PreferenceManager.GetDefaultSharedPreferences(Application.Context).Edit();
            editor.PutBoolean(IS_DARK_THEME, IsEnabled);
            editor.Apply();
        }

        public void SaveIsNotificationsEnabled(bool IsEnabled)
        {
            ISharedPreferencesEditor editor = PreferenceManager.GetDefaultSharedPreferences(Application.Context).Edit();
            editor.PutBoolean(IS_NOTIFICATIONS_ENABLED, IsEnabled);
            editor.Apply();
        }

        public string GetLastPostTitle(Pages page)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);

            string result = prefs.GetString(PageToKey(page), "null");

            if (result == "null")
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public void SaveLastPostTitle(Pages page, string lastPostTitle)
        {
            ISharedPreferencesEditor editor = PreferenceManager.GetDefaultSharedPreferences(Application.Context).Edit();
            editor.PutString(PageToKey(page), lastPostTitle);
            editor.Apply();
        }

        private string PageToKey(Pages page)
        {
            switch (page)
            {
                case Pages.SurrenderHome: return LAST_POST_KEY_SURRENDER;
                case Pages.Official: return LAST_POST_KEY_SURRENDER;
            }
            return null;
        }
    }
}