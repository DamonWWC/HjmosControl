using System;
using System.Globalization;
using System.Windows.Data;

namespace Hjmos.BaseControls.Tools.Converter
{
    /// <summary>
    /// 数值*100
    /// </summary>
    public class HundredFoldConverter : IValueConverter
    {
        public static readonly HundredFoldConverter Instance = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => ((double)value) * 100;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ((double)value) * 100;
    }
}