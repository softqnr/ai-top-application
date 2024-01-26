using System.Globalization;

namespace AiTopStudentStatus.Converters
{
    public class ListViewHeaderVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return false;

            var isLoading = bool.Parse(values[0].ToString());
            var isListEmpty = bool.Parse(values[1].ToString());

            if (isLoading || isListEmpty)
                return false;

            return true;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}