using Surrender_20.Core.Interface;
using Xamarin.Forms;

namespace Surrender_20.Forms.Services
{
    public class ThemeService : IThemeService
    {
        public void SetAppTheme(AppTheme appTheme)
        {
            switch (appTheme)
            {
                case AppTheme.Dark: SetDarkTheme(); break;
                case AppTheme.Ligt: SetLightTheme(); break;
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