using LeagueOfNews.Core.Interface;
using System.Collections.Generic;

namespace LeagueOfNews.Core.Service
{
    public abstract class AbstractSettingsService : ISettingsService
    {
        protected readonly Dictionary<NewsCategory, CategoryData> categories;

        public AbstractSettingsService()
        {
            categories = new Dictionary<NewsCategory, CategoryData>();

            this[NewsCategory.None] = new CategoryData { Title = "Settings" };

            this[NewsCategory.SurrenderHome] = new CategoryData { Title = "Home", CategoryUrl = "https://www.surrenderat20.net/", Website = NewsWebsite.Surrender };
            this[NewsCategory.PBE] = new CategoryData { Title = "PBE", CategoryUrl = "https://www.surrenderat20.net/search/label/PBE/", Website = NewsWebsite.Surrender };
            this[NewsCategory.Releases] = new CategoryData { Title = "Releases", CategoryUrl = "https://www.surrenderat20.net/search/label/Releases", Website = NewsWebsite.Surrender };
            this[NewsCategory.RedPosts] = new CategoryData { Title = "Red Posts", CategoryUrl = "https://www.surrenderat20.net/search/label/Red%20Posts", Website = NewsWebsite.Surrender };
            this[NewsCategory.Rotations] = new CategoryData { Title = "Rotations", CategoryUrl = "https://www.surrenderat20.net/search/label/Rotations", Website = NewsWebsite.Surrender };
            this[NewsCategory.ESports] = new CategoryData { Title = "E-Sports", CategoryUrl = "https://www.surrenderat20.net/search/label/Esports", Website = NewsWebsite.Surrender };
            this[NewsCategory.Official] = new CategoryData { Title = "League of Legends Official", CategoryUrl = "https://eune.leagueoflegends.com/en/news", Website = NewsWebsite.LoL };
            this[NewsCategory.Dev] = new CategoryData { Title = "Dev", CategoryUrl = "https://eune.leagueoflegends.com/en/news", Website = NewsWebsite.LoL };
        }

        public CategoryData this[NewsCategory Category]
        {
            get => categories.TryGetValue(Category, out CategoryData value) ? value : null;
            set => categories.Add(Category, value);
        }

        public WebsiteHistoryData WebsiteHistoryData { get; set; }

        public abstract ApplicationTheme Theme { get; set; }
        public abstract int NewPostCheckFrequency { get; set; }
        public abstract bool HasNotificationsEnabled { get; set; }
    }
}