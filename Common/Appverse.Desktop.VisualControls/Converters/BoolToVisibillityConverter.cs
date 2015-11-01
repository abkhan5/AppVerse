#region Namespace
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
#endregion

namespace Appverse.Desktop.VisualControls.Converters
{
    public   class BoolToVisibillityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = Visibility.Collapsed;
            bool? inputValue = value as bool?;
            if (inputValue.HasValue)
            {
                visibility = inputValue.Value ? Visibility.Visible : Visibility.Hidden;
            }

            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
