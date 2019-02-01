using System;
using System.Net.NetworkInformation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Surrender_20.UWP.Views.MessageBoxes
{
    public sealed partial class ConnectionDialog : ContentDialog
    {
        public ConnectionDialog()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();

            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            if (isInternetConnected == false)
            {
                ConnectionDialog Dialog = new ConnectionDialog();
                await Dialog.ShowAsync();
            }
            else
            {
                Hide();
            }
        }
    }
}
