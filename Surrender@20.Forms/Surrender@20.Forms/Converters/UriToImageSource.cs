using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Surrender_20.Forms.Converters
{
    public class UriToImageSource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentException("Converted value is null");

            if (!(value is Uri))
            {
                ImageSource src = value as String;
                return src;
            }
            return ImageSource.FromUri((Uri)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
