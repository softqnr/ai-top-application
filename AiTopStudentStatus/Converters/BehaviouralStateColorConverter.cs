using System.Globalization;

namespace AiTopStudentStatus.Converters
{
    public class BehaviouralStateColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int state = (int)value;
            switch (state)
            {
                case 1:
                    return Color.FromArgb("#108A43");
                case 2:
                    return Color.FromArgb("#FDBC26");
                case 3:
                    return Color.FromArgb("#EE3E35");
                default:
                    return Color.FromArgb("#108A43");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
