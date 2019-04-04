using LeagueOfNews.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace LeagueOfNews.UWP.Converters
{
    public class WebsiteToNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is NewsWebsite)
            {
                return (int) value;
            }

            throw new ArgumentException("Given object is not NewsWebsite enum");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
