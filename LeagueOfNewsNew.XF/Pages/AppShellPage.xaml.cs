using System.Runtime.CompilerServices;
using LeagueOfNewsNew.XF.PageModels;
using Xamarin.Forms;

namespace LeagueOfNewsNew.XF.Pages
{
    public partial class AppShellPage : Shell
    {
        public AppShellPage()
        {
            InitializeComponent();
            this.SetPageModel();
        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "Websites")
            {

            }
        }
    }
}