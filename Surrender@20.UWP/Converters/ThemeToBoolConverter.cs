using Surrender_20.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Surrender_20.UWP.Converters
{
    public class ThemeToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            ApplicationTheme theme = ApplicationTheme.Default;
            switch (parameter as string)
            {
                case "Dark": theme = ApplicationTheme.Dark; break;
                case "Light": theme = ApplicationTheme.Light; break;
            }

            return (ApplicationTheme) value == theme;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            ApplicationTheme theme = ApplicationTheme.Default;
            switch (parameter as string)
            {
                case "Dark": theme = ApplicationTheme.Dark; break;
                case "Light": theme = ApplicationTheme.Light; break;
            }

            return theme;
        }
    }
}
