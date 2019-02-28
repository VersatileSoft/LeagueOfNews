using PropertyChanged;
using Surrender_20.Core.Interface;

namespace Surrender_20.Model
{
    [AddINotifyPropertyChangedInterface]
    public class Newsfeed
    {
        public string UrlToNewsfeed { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public byte[] Image { get; set; }
        public NewsCategory Page { get; set; }
        public NewsWebsite Website { get; set; }
    }
}