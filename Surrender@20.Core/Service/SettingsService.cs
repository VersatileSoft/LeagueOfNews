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
        private readonly Dictionary<Pages, NewsfeedNavigationParameter> settings;

        public SettingsService()
        {
            settings = new Dictionary<Pages, NewsfeedNavigationParameter>();

            this[Pages.SurrenderHome] = new NewsfeedNavigationParameter { Title = "Home", URL = "https://www.surrenderat20.net/" };
            this[Pages.PBE] = new NewsfeedNavigationParameter { Title = "PBE", URL = "https://www.surrenderat20.net/search/label/PBE/" };
            this[Pages.Releases] = new NewsfeedNavigationParameter { Title = "Releases", URL = "https://www.surrenderat20.net/search/label/Releases" };
            this[Pages.RedPosts] = new NewsfeedNavigationParameter { Title = "Red Posts", URL = "https://www.surrenderat20.net/search/label/Red%20Posts" };
            this[Pages.Rotations] = new NewsfeedNavigationParameter { Title = "Rotations", URL = "https://www.surrenderat20.net/search/label/Rotations" };
            this[Pages.ESports] = new NewsfeedNavigationParameter { Title = "E-Sports", URL = "https://www.surrenderat20.net/search/label/Esports" };
            this[Pages.Official] = new NewsfeedNavigationParameter { Title = "League of Legends Official", URL = "https://eune.leagueoflegends.com/en/news" };
           // this[Pages.Dev] = new NewsfeedNavigationParameter { Title = "Dev", URL = "https://eune.leagueoflegends.com/en/news" };

        }

        public NewsfeedNavigationParameter this[Pages PropertyName]
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
            set => settings.Add(PropertyName, value);
        }
    }
}