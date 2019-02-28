using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Core.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surrender_20.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true)]
    public partial class SettingsView : MvxContentPage<SettingsViewModel>
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private void WindowsAppButton_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.google.com"));
        }
    }
}