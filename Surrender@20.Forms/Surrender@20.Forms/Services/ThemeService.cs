using Surrender_20.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;

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

           // App.Current.Resources["ListColor"] = App.Current.Resources["LightListColor"];
        }

        private void SetDarkTheme()
        {
            //TODO setting ligt theme

           // App.Current.Resources["ListColor"] = App.Current.Resources["DarkListColor"];
        }
    }
}
