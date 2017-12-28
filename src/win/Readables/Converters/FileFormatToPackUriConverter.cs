﻿using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Readables.Converters
{
    public class FileFormatToPackUriConverter : IValueConverter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is String str)
            {
                switch (str)
                {
                    case "epub":
                        return "pack://application:,,,/Resources/format_epub.png";
                    case "comic":
                        return "pack://application:,,,/Resources/format_comic.png";
                    case "mobi":
                        return "pack://application:,,,/Resources/format_azw3.png";
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
