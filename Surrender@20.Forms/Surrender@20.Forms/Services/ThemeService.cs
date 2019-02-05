using Surrender_20.Core.Interface;

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
            //TODO setting ligt theme

            App.Current.Resources["Frame"] = App.Current.Resources["LightFrame"];
        }

        private void SetDarkTheme()
        {
            //TODO setting ligt theme

             App.Current.Resources["Frame"] = App.Current.Resources["DarkFrame"];
        }
    }
}
