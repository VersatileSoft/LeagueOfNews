using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LeagueOfNews.UWP.Views.MessageBoxes
{
    public sealed partial class ConnectionDialog : ContentDialog
    {
        public Func<bool> HasInternetConnection { get; set; }

        public ConnectionDialog()
        {
            InitializeComponent();
        }

        public async void Execute(Func<bool> hasInternetConnection)
        {
            HasInternetConnection = hasInternetConnection;

            if (!hasInternetConnection.Invoke())
            {
                await ShowAsync();
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Hide();

            if (!HasInternetConnection.Invoke())
            {
                ConnectionDialog Dialog = new ConnectionDialog();
                Dialog.Execute(HasInternetConnection);
            }
            //TODO: else - request download menu content if not loaded
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Application.Current.Exit();
        }
    }
}