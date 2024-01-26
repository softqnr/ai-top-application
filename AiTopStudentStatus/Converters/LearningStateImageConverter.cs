using System.Globalization;

namespace AiTopStudentStatus.Converters
{
    public class LearningStateImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int state = (int)value;
            return ImageSource.FromResource($"AiTopStudentStatus.Resources.Images.LearningStatuses.{state}.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
