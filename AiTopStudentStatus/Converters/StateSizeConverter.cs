using System.Globalization;

namespace AiTopStudentStatus.Converters
{
    public class StateSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int state = (int)value;

            switch (state)
            {
                case 1:
                    return 15;
                case 2:
                    return 25;
                case 3:
                    return 35;
                default:
                    return 15;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
