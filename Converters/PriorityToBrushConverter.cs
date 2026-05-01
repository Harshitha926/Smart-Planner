using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using SmartPlanner.Models;

namespace SmartPlanner.Converters
{
    public class PriorityToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is PriorityLevel priority
                ? priority switch
                {
                    PriorityLevel.High => new SolidColorBrush(Color.FromRgb(239, 68, 68)),
                    PriorityLevel.Medium => new SolidColorBrush(Color.FromRgb(249, 115, 22)),
                    PriorityLevel.Low => new SolidColorBrush(Color.FromRgb(34, 197, 94)),
                    _ => new SolidColorBrush(Color.FromRgb(79, 70, 229)),
                }
                : new SolidColorBrush(Color.FromRgb(79, 70, 229));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
