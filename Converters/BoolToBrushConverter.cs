using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SmartPlanner.Converters
{
    public class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isActive = value is bool active && active;
            return isActive ? new SolidColorBrush(Color.FromRgb(248, 113, 113)) : Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
