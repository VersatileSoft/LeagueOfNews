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

            this[Setting.Home] = new NewsfeedNavigationParameter { Title = "Home", URL = "https://www.surrenderat20.net/?m=1" };
            this[Setting.PBE] = new NewsfeedNavigationParameter { Title = "PBE", URL = "https://www.surrenderat20.net/search/label/PBE?m=1" };
            this[Setting.Releases] = new NewsfeedNavigationParameter { Title = "Releases", URL = "https://www.surrenderat20.net/search/label/Releases?m=1" };
            this[Setting.RedPosts] = new NewsfeedNavigationParameter { Title = "Red Posts", URL = "https://www.surrenderat20.net/search/label/Red%20Posts?m=1" };
            this[Setting.Rotations] = new NewsfeedNavigationParameter { Title = "Rotations", URL = "https://www.surrenderat20.net/search/label/Rotations?m=1" };
            this[Setting.ESports] = new NewsfeedNavigationParameter { Title = "E-Sports", URL = "https://www.surrenderat20.net/search/label/Esports?m=1" };
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
