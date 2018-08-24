using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Net.NetworkInformation;
using System;

namespace Surrender_20.UWP.Views.MessageBoxes
{
    public sealed partial class ConnectionDialog : ContentDialog
    {      
        public ConnectionDialog()
        {
            this.InitializeComponent();         
        }      

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            if (isInternetConnected == false)
            {
                ConnectionDialog Dialog = new ConnectionDialog();
                await Dialog.ShowAsync();
            }
            else
            {
                this.Hide();
            }
        }
    }
}
