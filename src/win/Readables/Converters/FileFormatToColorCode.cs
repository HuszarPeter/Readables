using NLog;
using Readables.Styles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Readables.Converters
{
    public class FileFormatToColorCode : IValueConverter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String str)
            {
                switch (str)
                {
                    case "epub":
                        return AppColors.EpubFormatBackgroundBrush;
                    case "comic":
                        return AppColors.ComicFormatBackgroundBrush;
                    case "mobi":
                        return AppColors.MobiFormatBackgroundBrush;
                    default:
                        logger.Warn($"Unknown file format '{str}'");
                        break;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
