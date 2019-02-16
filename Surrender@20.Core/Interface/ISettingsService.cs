using Surrender_20.Core.Service;
using System;

namespace Surrender_20.Core.Interface
{
    public interface ISettingsService
    {
        CategoryData this[NewsCategory Category] { get; set; }

        ApplicationTheme Theme { get; set; }
        int NewPostCheckFrequency { get; set; }
        bool HasNotificationsEnabled { get; set; }

        void SaveLastPostTitle(NewsCategory page, string title);
    }

    public class CategoryData
    {
        public string Title { get; set; }
        public string LastPostTitle { get; internal set; }
        public string CategoryURL { get; set; } //Should be Uri or string?

        public NewsCategory Category { get; set; } = NewsCategory.None;
        public NewsWebsite Website { get; set; } = NewsWebsite.None;
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