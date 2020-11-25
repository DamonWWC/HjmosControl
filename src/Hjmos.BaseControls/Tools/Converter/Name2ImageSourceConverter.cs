using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hjmos.BaseControls.Tools.Converter
{
    public class Name2ImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = @"pack://application:,,,/Hjmos.BaseControls;component/Resources/WeatherImage/";
            if (value is string str)
            {

                var resoult = $"{path}{str}.svg";
                return resoult;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
