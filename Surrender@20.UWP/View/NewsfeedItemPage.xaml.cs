using Windows.UI.Xaml.Controls;
using Surrender_20.Core.ViewModel;

namespace Surrender_20.View
{
    public sealed partial class NewsfeedItemPage : Page
    {
        public MainPageViewModel VM { get; set; }

        public NewsfeedItemPage()
        {
            this.InitializeComponent();

            VM = DataContext as MainPageViewModel;
        }
    }
}
