using PropertyChanged;
using System.Windows.Input;

namespace Surrender_20.Core.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class MainPageViewModel
    {     
        public string Title { get; set; } = "Home";

        public ICommand HomeCommand { get; private set; }
        public ICommand PBECommand { get; private set; }
        public ICommand ReleasesCommand { get; private set; }
        public ICommand RedPostsCommand { get; private set; }
        public ICommand RotationsCommand { get; private set; }
        public ICommand EsportsCommand { get; private set; }
    }  
}
