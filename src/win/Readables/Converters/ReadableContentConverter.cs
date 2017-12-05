using Readables.UI.Model;
using Readables.View.Cover;
using Readables.View.List;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Readables.Converters
{
    public class ReadableContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is ViewMode cm)
            {
                switch (cm)
                {
                    case ViewMode.List:
                        return new ReadableListView();
                    case ViewMode.Cover:
                        return new ReadableCoverView();
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
