using LeagueOfNews.Core.Interface;
using PropertyChanged;

namespace LeagueOfNews.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Newsfeed
    {
        public string UrlToNewsfeed { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public byte[] Image { get; set; }
        public string ImageUri { get; set; }
        public NewsCategory Page { get; set; }
        public NewsWebsite Website { get; set; }
    }
}