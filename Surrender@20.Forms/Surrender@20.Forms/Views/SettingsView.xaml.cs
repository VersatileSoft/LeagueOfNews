using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Forms.ViewModels;
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
    }
}