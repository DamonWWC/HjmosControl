using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Hjmos.BaseControls.Tools.Converter
{
    public class Text2ForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null&&value.ToString().EndsWith("%")&&double.TryParse(value.ToString().TrimEnd('%'),out double text))
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString(text > 0 ? "#FFD35C62" : "#FF11D4FF"));
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
