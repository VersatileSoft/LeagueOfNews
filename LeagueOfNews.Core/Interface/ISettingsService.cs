using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LeagueOfNews.Core.Interface
{
    public interface ISettingsService
    {
        CategoryData this[NewsCategory Category] { get; set; }
        ApplicationTheme Theme { get; set; }
        int NewPostCheckFrequency { get; set; }
        bool HasNotificationsEnabled { get; set; }
        WebsiteHistoryData WebsiteHistoryData { get; }
    }

    public class CategoryData
    {
        public string Title { get; set; }
        public string CategoryUrl { get; set; }
        public NewsWebsite Website { get; set; } = NewsWebsite.None;
    }

    public class WebsiteHistoryData
    {
        public delegate void OnTitleChange(PostTitleArgs args);
        public event OnTitleChange TitleChanged;

        private string _lastSurrenderPostUrl;
        private string _lastOfficialPostUrl;

        public string LastSurrenderPostUrl
        {
            get => _lastSurrenderPostUrl;
            set
            {
                _lastSurrenderPostUrl = value;
                TitleChanged?.Invoke(new PostTitleArgs { Category = NewsWebsite.Surrender, Title = value });
            }        
        }
        public string LastOfficialPostUrl
        {
            get => _lastOfficialPostUrl;
            set
            {
                _lastSurrenderPostUrl = value;
                TitleChanged?.Invoke(new PostTitleArgs { Category = NewsWebsite.LoL, Title = value });
            }            
        }

        public class PostTitleArgs
        {
            public NewsWebsite Category { get; set; }
            public string Title { get; set; }
        }
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