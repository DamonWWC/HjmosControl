using Hjmos.BaseControls.Expression.Drawing;
using Hjmos.BaseControls.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hjmos.BaseControls.Tools.Converter
{
    public class Number2PercentageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length != 2) return .0;

            var obj1 = values[0];
            var obj2 = values[1];

            if (obj1 == null || obj2 == null) return .0;

            var str1 = values[0].ToString();
            var str2 = values[1].ToString();

            var v1 = str1.Value<double>();
            var v2 = str2.Value<double>();

            if (MathHelper.IsVerySmall(v2)) return 100.0;

            return v1 / v2 * 100;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
