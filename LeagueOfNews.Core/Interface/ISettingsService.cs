using System.Collections.Generic;

namespace LeagueOfNews.Core.Interface
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
        public string CategoryUrl { get; set; }
        public NewsWebsite Website { get; set; } = NewsWebsite.None;
    }

    public class WebsiteHistoryData
    {
        public virtual string LastSurrenderPostUrl { get; set; }
        public virtual string LastOfficialPostUrl { get; set; }
        public virtual List<string> VisitedPosts { get; set; }
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