using Surrender_20.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

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

            this[Setting.Home] = new NewsfeedNavigationParameter { Title = "Home", URL = "http://feeds.feedburner.com/surrenderat20/home?format=html" };
            this[Setting.PBE] = new NewsfeedNavigationParameter { Title = "PBE", URL = "http://feeds.feedburner.com/surrenderat20/pbe?format=html" };
            this[Setting.Releases] = new NewsfeedNavigationParameter { Title = "Releases", URL = "http://feeds.feedburner.com/surrenderat20/releases?format=html" };
            this[Setting.RedPosts] = new NewsfeedNavigationParameter { Title = "Red Posts", URL = "http://feeds.feedburner.com/surrenderat20/red_posts?format=html" };
            this[Setting.People] = new NewsfeedNavigationParameter { Title = "People", URL = "http://feeds.feedburner.com/surrenderat20/rotations?format=html" };
            this[Setting.ESports] = new NewsfeedNavigationParameter { Title = "E-Sports", URL = "http://feeds.feedburner.com/surrenderat20/esports?format=html" };
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
