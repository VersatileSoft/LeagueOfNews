using System.Collections.Generic;

namespace Surrender_20.Core.Interface
{
    public interface ISettingsService
    {
        CategoryData this[NewsCategory Category] { get; set; }

        ApplicationTheme Theme { get; set; }
        int NewPostCheckFrequency { get; set; }
        bool HasNotificationsEnabled { get; set; }

        WebsiteHistoryData WebsiteHistoryData { get; set; }
    }

    public class CategoryData
    {
        public string Title { get; set; }
        public string CategoryURL { get; set; }

        public NewsCategory Category { get; set; } = NewsCategory.None;
        public NewsWebsite Website { get; set; } = NewsWebsite.None;
    }

    public class WebsiteHistoryData
    {
        public string LastPostSurrenderUrl { get; set; }
        public string LastPostOfficialUrl { get; set; }
        public List<string> VisitedPosts { get; set; } = new List<string>();
    }

    public enum NewsCategory
    {
        None = -1,

        SurrenderHome, //Surrender@20
        PBE,           //
        Releases,      //
        RedPosts,      //
        Rotations,     //
        ESports,       //

        Official,      //League of Legends (official website & forum)
        Dev            //
    }

    public enum NewsWebsite
    {
        None = -1,

        Surrender,    //Surrender@20
        LoL           //League of Legends (official website & forum)
    }

    public enum ApplicationTheme
    {
        Default = -1,

        Light,
        Dark
    }
}