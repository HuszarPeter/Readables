using System;
using System.Globalization;
using System.Windows.Data;

namespace Readables.Converters
{
    public class HalfValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Double dblValue)
            {
                return dblValue / 2;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Double dblValue)
            {
                return dblValue * 2;
            }
            return value;
        }
    }
}
