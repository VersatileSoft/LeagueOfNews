using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Surrender_20.UWP.Views.MessageBoxes
{
    public sealed partial class SiteChooseDialog : ContentDialog
    {
        public SiteChooseDialog()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
