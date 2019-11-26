using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using Xamarin.Forms;

namespace MyContactList.Converters
{
    public class NotNullOrEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is string) ? ((string)value).Length > 0 : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
