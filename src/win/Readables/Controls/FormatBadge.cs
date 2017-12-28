using System;
using System.Windows;
using System.Windows.Controls;

namespace Readables.Controls
{
    public class FormatBadge : Control
    {
        public String Format
        {
            get { return (String)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Format.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(String), typeof(FormatBadge), new PropertyMetadata(String.Empty));

        static FormatBadge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormatBadge), new FrameworkPropertyMetadata(typeof(FormatBadge)));
        }
    }
}
