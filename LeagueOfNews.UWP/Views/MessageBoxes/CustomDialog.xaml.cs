using Windows.UI.Xaml.Controls;

namespace LeagueOfNews.UWP.Views.MessageBoxes
{
    public sealed partial class CustomDialog : ContentDialog
    {
        public enum Type
        {
            Error,
            Warning,
            Message
        }

        public CustomDialog(string message, string errorcode, Type type = Type.Message)
        {
            InitializeComponent();

            MessageText.Text = message;

            if (errorcode != null)
            {
                ErrorCodeText.Text = "Error code: " + errorcode;
            }

            switch (type)
            {
                case Type.Error:
                    Title = "Error";
                    break;
                case Type.Warning:
                    Title = "Warrning";
                    break;
                case Type.Message:
                    Title = "Message";
                    break;
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();
        }
    }
}