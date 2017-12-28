using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Readables.Styles
{
    public static class AppColors
    {
        public static Color EpubFormatColor => Color.FromRgb(80, 227, 194);

        public static Color MobiFormatColor => Color.FromRgb(184, 233, 134);

        public static Color ComicFormatColor => Color.FromRgb(126, 211, 33);

        public static Brush EpubFormatBackgroundBrush => new SolidColorBrush(EpubFormatColor);

        public static Brush MobiFormatBackgroundBrush => new SolidColorBrush(MobiFormatColor);

        public static Brush ComicFormatBackgroundBrush => new SolidColorBrush(ComicFormatColor);
    }
}
