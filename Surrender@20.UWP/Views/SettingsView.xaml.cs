using MvvmCross.Platforms.Uap.Views;
using Surrender_20.Core.ViewModels;

namespace Surrender_20.UWP.View
{
    public sealed partial class SettingsView : MvxWindowsPage
    {
        public SettingsViewModel VM => ViewModel as SettingsViewModel;

        public SettingsView()
        {
            this.InitializeComponent();
        }
    }
}
