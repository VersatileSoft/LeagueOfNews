using LeagueOfNews.Core.Interface;
using PropertyChanged;

namespace LeagueOfNews.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Newsfeed
    {
#nullable enable
        public string? UrlToNewsfeed { get; set; }
        public string? Date { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public byte[]? Image { get; set; }
        public string? ImageUri { get; set; }
#nullable disable
        public NewsCategory Page { get; set; }
        public NewsWebsite Website { get; set; }
    }
}