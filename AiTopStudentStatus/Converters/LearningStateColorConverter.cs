using System.Globalization;

namespace AiTopStudentStatus.Converters
{
    public class LearningStateColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int state = (int)value;
            switch (state)
            {
                case 1:
                    return Color.FromArgb("#3A6DB5");
                case 2:
                    return Color.FromArgb("#84837A");
                case 3:
                    return Color.FromArgb("#EE3E35");
                default:
                    return Color.FromArgb("#3A6DB5");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
