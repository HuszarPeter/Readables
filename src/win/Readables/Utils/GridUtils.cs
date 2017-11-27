using Readables.Common.Extensions;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Readables.Utils
{
    class GridUtils
    {
        public static String GetColumns(DependencyObject obj)
        {
            return (String)obj.GetValue(ColumnsProperty);
        }

        public static void SetColumns(DependencyObject obj, String value)
        {
            obj.SetValue(ColumnsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Columns.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.RegisterAttached(
                "Columns", 
                typeof(String), 
                typeof(GridUtils), 
                new PropertyMetadata("", OnColumnsChanged));



        public static String GetRows(DependencyObject obj)
        {
            return (String)obj.GetValue(RowsProperty);
        }

        public static void SetRows(DependencyObject obj, String value)
        {
            obj.SetValue(RowsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached(
                "Rows", 
                typeof(String), 
                typeof(GridUtils), 
                new PropertyMetadata("", OnRowsChanged));

        private static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Grid grid)
            {
                grid.ColumnDefinitions.Clear();
                if (e.NewValue is String str)
                {
                    str.ParseAsGridLengths()
                        .Select(gl => new ColumnDefinition { Width = gl })
                        .ForEach(colDef => grid.ColumnDefinitions.Add(colDef));
                }
            }
        }

        private static void OnRowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Grid grid)
            {
                grid.RowDefinitions.Clear();
                if (e.NewValue is String str)
                {
                    str.ParseAsGridLengths()
                        .Select(gl => new RowDefinition { Height = gl })
                        .ForEach(rowDef => grid.RowDefinitions.Add(rowDef));
                }
            }
        }
    }

    public static class StringExtensions
    {
        public static GridLength[] ParseAsGridLengths(this string str)
        {
            var values = str.Split(new[] { ';', '|' });
            return values.Select(value =>
            {
                return value.ParseAsGridLength();
            }).ToArray();
        }

        public static GridLength ParseAsGridLength(this string str)
        {
            if (String.IsNullOrEmpty(str) || str.Equals("auto", StringComparison.InvariantCultureIgnoreCase))
            {
                return GridLength.Auto;
            }
            var s = str.Trim();

            if(double.TryParse(s, out double dbl))
            {
                return new GridLength(dbl);
            }

            if (s == "*")
            {
                return new GridLength(1, GridUnitType.Star);
            }

            if (double.TryParse(s.Substring(0, s.Length-1), out double pixel))
            {
                return new GridLength(pixel, GridUnitType.Star);
            }

            throw new Exception($"Unknown format {str}");
        }
    }
}
