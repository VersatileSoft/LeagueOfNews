using LeagueOfNews.Core.Interface;
using LeagueOfNews.Core.Service;
using Xamarin.Forms;

namespace LeagueOfNews.Forms.Services
{
    public abstract class FormsSettingsService : AbstractSettingsService
    {

        public FormsSettingsService() : base()
        {
            SetAppTheme();
        }

        protected void SetAppTheme()
        {
            switch (Theme)
            {
                case ApplicationTheme.Dark: SetDarkTheme(); break;
                case ApplicationTheme.Light: SetLightTheme(); break;
                default: SetDarkTheme(); break;
            }
        }

        private void SetLightTheme()
        {
            Application.Current.Resources["Page"] = Application.Current.Resources["LightPage"];
            Application.Current.Resources["Frame"] = Application.Current.Resources["LightFrame"];
            Application.Current.Resources["Label"] = Application.Current.Resources["LightLabel"];
            Application.Current.Resources["MenuLabel"] = Application.Current.Resources["LightMenuLabel"];
            Application.Current.Resources["Image"] = Application.Current.Resources["LightImage"];
            Application.Current.Resources["DescriptionLabel"] = Application.Current.Resources["LightDescriptionLabel"];
        }
        private void SetDarkTheme()
        {
            Application.Current.Resources["Page"] = Application.Current.Resources["DarkPage"];
            Application.Current.Resources["Frame"] = Application.Current.Resources["DarkFrame"];
            Application.Current.Resources["Label"] = Application.Current.Resources["DarkLabel"];
            Application.Current.Resources["MenuLabel"] = Application.Current.Resources["DarkMenuLabel"];
            Application.Current.Resources["Image"] = Application.Current.Resources["DarkImage"];
            Application.Current.Resources["DescriptionLabel"] = Application.Current.Resources["DarkDescriptionLabel"];
        }
    }
}