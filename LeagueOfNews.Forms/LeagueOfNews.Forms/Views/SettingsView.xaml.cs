using LeagueOfNews.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using Xamarin.Essentials;

namespace LeagueOfNews.Forms.Views
{
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Detail, NoHistory = true)]
    public partial class SettingsView : MvxContentPage<SettingsViewModel>
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        private async void WindowsAppButton_Clicked(object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri("https://www.microsoft.com/store/apps/9N06TGN05XNK"));
        }
    }
}