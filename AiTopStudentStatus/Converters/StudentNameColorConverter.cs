using System.Globalization;

namespace AiTopStudentStatus.Converters
{
    public class StudentNameColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return Colors.Black;

            var behaviouralState = int.Parse(values[0].ToString());
            var learningState = int.Parse(values[1].ToString());

            if (behaviouralState == 3 && learningState == 3)
                return Colors.Red;

            return Colors.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}