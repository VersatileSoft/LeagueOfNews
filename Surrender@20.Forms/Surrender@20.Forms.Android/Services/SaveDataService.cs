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

        public int GetCheckNewPostsFrequency()
        {
            return PreferenceManager.GetDefaultSharedPreferences(Application.Context).GetInt(CHECK_NEW_POSTS_FREQUENCY, 1);
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
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            ISharedPreferencesEditor editor = prefs.Edit();
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