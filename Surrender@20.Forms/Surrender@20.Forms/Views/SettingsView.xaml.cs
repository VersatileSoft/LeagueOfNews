using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Surrender_20.Forms.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Surrender_20.Forms.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class SettingsView : MvxContentPage<SettingsViewModel>
    {
        public SettingsView()
        {
            InitializeComponent();
        }
    }
}