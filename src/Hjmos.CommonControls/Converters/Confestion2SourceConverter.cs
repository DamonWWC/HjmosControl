using Hjmos.CommonControls.Tools;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Hjmos.CommonControls.Converters
{
    public class Confestion2SourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null && value is ConfestionStatus confestionStatus)
            {
                ImageSource imageSource = new ImageSource();
                return new Uri(imageSource.GetValue(confestionStatus));
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
