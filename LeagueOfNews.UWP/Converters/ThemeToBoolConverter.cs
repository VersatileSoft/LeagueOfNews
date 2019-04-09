using LeagueOfNews.Core.Interface;
using System;
using Windows.UI.Xaml.Data;

namespace LeagueOfNews.UWP.Converters
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

            return (ApplicationTheme)value == theme;
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