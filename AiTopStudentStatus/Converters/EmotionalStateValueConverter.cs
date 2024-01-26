using AiTopStudentStatus.Enums;
using System.Globalization;

namespace AiTopStudentStatus.Converters
{
    public class EmotionalStateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (int)value;
            return (EmotionalStates)state;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}