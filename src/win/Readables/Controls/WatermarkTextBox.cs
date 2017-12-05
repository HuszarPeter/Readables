using System;
using System.Windows;
using System.Windows.Controls;

namespace Readables.Controls
{
    public class WatermarkTextBox : TextBox
    {
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public bool HasText
        {
            get { return (bool)GetValue(HasTextProperty); }
        }

        static WatermarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(WatermarkTextBox), 
                new FrameworkPropertyMetadata(typeof(WatermarkTextBox)));

            TextProperty.OverrideMetadata(
                typeof(WatermarkTextBox), 
                new FrameworkPropertyMetadata(OnTextPropertyChanged));
        }

        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register(
                "Watermark", 
                typeof(string), 
                typeof(WatermarkTextBox), 
                new PropertyMetadata(String.Empty));

        private static readonly DependencyPropertyKey HasTextPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "HasText", 
                typeof(bool), 
                typeof(WatermarkTextBox), 
                new PropertyMetadata(false));

        public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WatermarkTextBox watermarkTextBox)
            {
                var isTextEntered = !String.IsNullOrEmpty(watermarkTextBox.Text);
                if (isTextEntered != watermarkTextBox.HasText)
                {
                    watermarkTextBox.SetValue(HasTextPropertyKey, isTextEntered);
                }
            }
        }
    }
}
