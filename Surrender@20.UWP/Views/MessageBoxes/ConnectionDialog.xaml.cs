using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Surrender_20.UWP.Views.MessageBoxes
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            if (!HasInternetConnection.Invoke())
            {
                ConnectionDialog Dialog = new ConnectionDialog();
                Dialog.Execute(HasInternetConnection);
            }
        }
    }
}