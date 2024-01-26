using AiTopStudentStatus.Enums;
using System.Globalization;

namespace AiTopStudentStatus.Converters
{
    public class BehaviouralStateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (int)value;
            return (BehaviouralStates)state;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}