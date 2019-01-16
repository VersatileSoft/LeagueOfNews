using Android.App;
using Android.Content;
using Android.Preferences;
using Surrender_20.Core.Interface;

namespace Surrender_20.Forms.Droid.Services
{
    public class SaveDataService : ISaveDataService
    {
        public string GetData(string key)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);

            string result = prefs.GetString(key, "null");
            if (result == "null")
                return null;
            else
                return result;
        }

        public void SaveData(string key, string data)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString(key, data);
            editor.Apply();
        }
    }
}