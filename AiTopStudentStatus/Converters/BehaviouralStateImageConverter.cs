using System.Globalization;

namespace AiTopStudentStatus.Converters
{
    public class BehaviouralStateImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int state = (int)value;
            return ImageSource.FromResource($"AiTopStudentStatus.Resources.Images.BehavioralStatuses.{state}.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
