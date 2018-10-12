using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using PropertyChanged;
using Surrender_20.Model;

namespace Surrender_20.Core.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class NewsfeedItemViewModel : MvxViewModel<Newsfeed>
    {
        public Newsfeed Content { get; set; }
        public string TextContent { get; set; }
        public string Title => Content.Title;

        public NewsfeedItemViewModel()
        {

        }

        public override void Prepare(Newsfeed newsfeed)
        {
            Content = newsfeed;

            TextContent = Content.Content.InnerText;

            
        }
    }
}
