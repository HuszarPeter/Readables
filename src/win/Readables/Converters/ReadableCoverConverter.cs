using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Readables.Converters
{
    public class ReadableCoverConverter : IValueConverter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private const string DefaultImagePackUri = "pack://application:,,,/Resources/default_cover.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is byte[] bytes)
            {
                try
                {
                    if (bytes == null || bytes.Length == 0)
                    {
                        return null;
                    }

                    var result = new BitmapImage();
                    using (var mem = new MemoryStream(bytes))
                    {
                        mem.Position = 0;
                        result.BeginInit();
                        result.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        result.CacheOption = BitmapCacheOption.OnLoad;
                        result.UriSource = null;
                        result.StreamSource = mem;
                        result.EndInit();
                    }
                    result.Freeze();
                    return result;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return DefaultImagePackUri;
                }
            }
            return DefaultImagePackUri;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
