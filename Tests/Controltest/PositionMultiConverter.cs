using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Controltest
{
    public class PositionMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(double.TryParse(parameter.ToString(),out double param))
            {
                var width = values[0] as double?;
                return new Thickness((double)(param - width * 0.5), 0, 0, 0);
            }
            return new Thickness(0);            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
