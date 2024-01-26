using System.Globalization;

namespace AiTopStudentStatus.Converters
{
    public class EmotionalStateImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int state = (int)value;
            return ImageSource.FromResource($"AiTopStudentStatus.Resources.Images.Emotions.{state}.jpg");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
