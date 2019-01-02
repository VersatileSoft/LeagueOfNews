using Surrender_20.Core.Interface;
using System.Collections.Generic;

namespace Surrender_20.Core.Service
{

    public class NewsfeedNavigationParameter
    {
        public string Title { get; set; }
        public string URL { get; set; }
    }

    public class SettingsService : ISettingsService
    {

        private Dictionary<Setting, NewsfeedNavigationParameter> settings;


        public SettingsService()
        {
            settings = new Dictionary<Setting, NewsfeedNavigationParameter>();

            this[Setting.Home] = new NewsfeedNavigationParameter { Title = "Home", URL = "https://www.surrenderat20.net/" };
            this[Setting.PBE] = new NewsfeedNavigationParameter { Title = "PBE", URL = "https://www.surrenderat20.net/search/label/PBE/" };
            this[Setting.Releases] = new NewsfeedNavigationParameter { Title = "Releases", URL = "https://www.surrenderat20.net/search/label/Releases" };
            this[Setting.RedPosts] = new NewsfeedNavigationParameter { Title = "Red Posts", URL = "https://www.surrenderat20.net/search/label/Red%20Posts" };
            this[Setting.Rotations] = new NewsfeedNavigationParameter { Title = "Rotations", URL = "https://www.surrenderat20.net/search/label/Rotations" };
            this[Setting.ESports] = new NewsfeedNavigationParameter { Title = "E-Sports", URL = "https://www.surrenderat20.net/search/label/Esports" };
            this[Setting.Official] = new NewsfeedNavigationParameter { Title = "Official", URL = "https://eune.leagueoflegends.com/pl/news" };

        }

        public NewsfeedNavigationParameter this[Setting PropertyName]
        {
            get
            {
                if (settings.TryGetValue(PropertyName, out NewsfeedNavigationParameter value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
            set { settings.Add(PropertyName, value); }
        }
    }
}
