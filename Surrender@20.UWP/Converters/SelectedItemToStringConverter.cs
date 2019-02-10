using System;
using Windows.UI.Xaml.Data;

namespace Surrender_20.UWP.Converters
{
    public class SelectedItemToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            dynamic Value = value;
            return Value?.SelectedItem?.Content;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}